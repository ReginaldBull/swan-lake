namespace MarktguruApi.Validation
{
    using FluentValidation;
    using JetBrains.Annotations;
    using MediatR.Commands;

    /// <summary>
    /// Validator for the CreateProductCommand.
    /// </summary>
    [UsedImplicitly]
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductCommandValidator"/> class.
        /// </summary>
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.CreateProductDto.Name)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.CreateProductDto.Price).GreaterThan(0);
        }
    }
}