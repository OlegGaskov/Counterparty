using Counterparty.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Counterparty.Api.ViewModels
{
    public class CounterpartyViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Fullname { get; set; }
        public ECounterpartyType Type { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(Name))
            {
                yield return new ValidationResult("'Name' field required!", new[] { nameof(Name) });
            }
            if (!Enum.IsDefined(typeof(ECounterpartyType), Type))
            {
                yield return new ValidationResult("'Acccount type' not valid!", new[] { nameof(Type) });
            }
            if (String.IsNullOrWhiteSpace(Inn))
            {
                yield return new ValidationResult("'Inn' field required!", new[] { nameof(Inn) });
            }
            if (Type == ECounterpartyType.Legal && string.IsNullOrWhiteSpace(Kpp))
            {
                yield return new ValidationResult("'Kpp' field required for Legal Entity!", new[] { nameof(Kpp) });
            }

        }
    }
}
