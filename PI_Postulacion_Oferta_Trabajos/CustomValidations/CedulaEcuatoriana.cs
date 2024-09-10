using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PI_Postulacion_Oferta_Trabajos.CustomValidations
{
    public class CedulaEcuatoriana : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Verifica si el valor no es nulo
            // La sintaxis value is string cedula realiza una verificación de tipo.
            // Si value es efectivamente un string, asigna value a la variable cedula.
            if (value == null || !(value is string cedula))
            {
                return new ValidationResult("El valor de la cédula no puede ser nulo.");
            }

            // Quita los espacios en blanco
            cedula = cedula.Trim();

            // Verifica que la cédula tenga exactamente 10 caracteres numéricos
            if (cedula.Length != 10 || !Regex.IsMatch(cedula, @"^\d{10}$"))
            {
                return new ValidationResult("La cédula ecuatoriana debe tener 10 dígitos numéricos.");
            }

            int codigoRegion = int.Parse(cedula.Substring(0, 2));
            if (codigoRegion < 1 || codigoRegion > 30)
            {
                return new ValidationResult("La cédula no pertenece a una región válida.");
            }

            // Validación del dígito verificador
            int[] coeficientes = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            int suma = 0;

            for (int i = 0; i < coeficientes.Length; i++)
            {
                int producto = int.Parse(cedula[i].ToString()) * coeficientes[i];
                suma += producto >= 10 ? producto - 9 : producto;
            }

            int digitoVerificador = 10 - (suma % 10);
            if (digitoVerificador == 10)
            {
                digitoVerificador = 0;
            }

            if (digitoVerificador != int.Parse(cedula[9].ToString()))
            {
                return new ValidationResult("La cédula ecuatoriana no es válida.");
            }

            return ValidationResult.Success;
        }
    }
}
