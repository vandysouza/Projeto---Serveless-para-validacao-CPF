using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace httpValidaCpf
{
    public static class fnvalidaCpf
    {
        [FunctionName("fnvalidaCpf")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Iniciando a validação do Cpf");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            if (data == null)
            {
                return new BadRequestObjectResult("Por favor, informe o cpf para validação");
            }

            string cpf = data?.cpf;
            bool valido = ValidarCpf(cpf);
            string responseMessage = valido ? "CPF válido" : "CPF inválido";

            return new OkObjectResult(responseMessage);
        }

        public static bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            var digits = new string((cpf ?? string.Empty).Where(char.IsDigit).ToArray());

            if (digits.Length != 11)
                return false;

            if (digits == "00000000000")
                return true;

            if (digits.Distinct().Count() == 1)
                return false;

            int[] numbers = digits.Select(c => c - '0').ToArray();

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += numbers[i] * (10 - i);

            int remainder = sum % 11;
            int firstCheck = remainder < 2 ? 0 : 11 - remainder;
            if (numbers[9] != firstCheck)
                return false;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += numbers[i] * (11 - i);

            remainder = sum % 11;
            int secondCheck = remainder < 2 ? 0 : 11 - remainder;
            if (numbers[10] != secondCheck)
                return false;

            return true;
        }
    }
}
