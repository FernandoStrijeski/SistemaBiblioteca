using System;
using System.ComponentModel.DataAnnotations;

namespace ORM.ValidacoesPersonalizadas
{
    public class EnumValueAttribute : ValidationAttribute
    {
        private readonly Type _enumType;

        public EnumValueAttribute(Type enumType)
        {
            _enumType = enumType;
        }

        public override bool IsValid(object? value)
        {
            // Allow null values
            if (value == null)
            {
                return true;
            }

            if (value is string stringValue)
            {
                Console.WriteLine("entrooou");
                return Enum.TryParse(_enumType, stringValue, true, out _);
            }

            // Check if the value is of the correct enum type
            return value.GetType() == _enumType && Enum.IsDefined(_enumType, value);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"O valor fornecido para {name} não é válido para o enum {_enumType.Name}.";
        }
    }
}
