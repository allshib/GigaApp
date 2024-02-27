using FluentValidation;
using FluentValidation.Results;
using GigaApp.Domain.Authentication;
using GigaApp.Domain.UseCases.SignOn;
using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Tests.SignOn
{
    public class SignOnUseCaseShould
    {
        private readonly ISetup<IPasswordManager, (byte[] Salt, byte[] Hash)> passwordManagerSetup;

        public SignOnUseCaseShould()
        {

            var storage = new Mock<ISignOnStorage>();
            var validator = new Mock<IValidator<SignOnCommand>>();
            validator
                .Setup(x => x.ValidateAsync(It.IsAny<SignOnCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var passwordManager = new Mock<IPasswordManager>();

            passwordManagerSetup = passwordManager
                .Setup(x => x.GeneratePasswordParts(It.IsAny<string>()));

            var sut = new SignOnUseCase(storage.Object, validator.Object,passwordManager.Object);
        }
    }
}
