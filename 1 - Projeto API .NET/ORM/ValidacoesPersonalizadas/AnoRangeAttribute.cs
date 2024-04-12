using System.ComponentModel.DataAnnotations;

namespace ORM.ValidacoesPersonalizadas
{
    public class AnoRangeAttribute : ValidationAttribute
    {
        private readonly short _minYear;
        private readonly short _maxYear;

        public AnoRangeAttribute(short minYear)
        {
            _minYear = minYear;
            _maxYear = (short) DateTime.Now.Year;
        }

        public override bool IsValid(object? value)
        {
            if (value is short ano)           
                return ano >= _minYear && ano <= _maxYear;            

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"O ano deve estar entre {_minYear} e {_maxYear}.";
        }
    }
}
