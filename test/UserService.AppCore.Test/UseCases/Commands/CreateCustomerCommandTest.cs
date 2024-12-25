using AutoMapper;
using Confluent.SchemaRegistry;
using Core.Domain;
using Core.Repository;
using Infrastructure.OutboxHandler;
using MediatR;
using Moq;
using NUnit.Framework;
using Services;
using System.Threading;
using System.Threading.Tasks;
using UserService.AppCore.Domain;
using UserService.AppCore.Domain.Outbox;
using UserService.AppCore.UseCases.Commands;
using UserService.AppCore.UseCases.Dtos;

namespace UserService.AppCore.Test.UseCases.Commands;

[TestFixture]
[TestOf(typeof(CreateCustomerCommand))]
public class CreateCustomerCommandTest
{
    private Mock<IMapper> _mapperMock;
    private Mock<ISchemaRegistryClient> _schemaRegistryClientMock;
    private Mock<IRepository<CustomerOutbox>> _customerOutboxRepositoryMock;
    private Mock<IRepository<Customer>> _customerRepositoryMock;
    private CreateCustomerCommand.Handler _handler;

    [SetUp]
    public void Setup()
    {
        _mapperMock = new Mock<IMapper>();
        _schemaRegistryClientMock = new Mock<ISchemaRegistryClient>();
        _customerOutboxRepositoryMock = new Mock<IRepository<CustomerOutbox>>();
        _customerRepositoryMock = new Mock<IRepository<Customer>>();

        _handler = new CreateCustomerCommand.Handler(
            _mapperMock.Object,
            _schemaRegistryClientMock.Object,
            _customerOutboxRepositoryMock.Object,
            _customerRepositoryMock.Object);
    }

    [Test]
    public async Task Handle_ShouldAddCustomerAndSendToOutbox()
    {
        // Arrange
        var command = new CreateCustomerCommand("John Doe", "john.doe@example.com", "123456789");
        var cancellationToken = CancellationToken.None;

        var customer = new Customer();
        customer.Create(command.FullName, command.Email, command.PhoneNumber);

        var customerDto = new CustomerDto { FullName = command.FullName, Email = command.Email, PhoneNumber = command.PhoneNumber };

        _mapperMock.Setup(m => m.Map<CustomerDto>(It.IsAny<Customer>()))
            .Returns(customerDto);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        _customerRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Customer>(), cancellationToken), Times.Once);
        _customerOutboxRepositoryMock.Verify(r => r.AddAsync(It.IsAny<CustomerOutbox>(), cancellationToken), Times.Once);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Data.FullName, Is.EqualTo(command.FullName));
        Assert.That(result.Data.Email, Is.EqualTo(command.Email));
        Assert.That(result.Data.PhoneNumber, Is.EqualTo(command.PhoneNumber));
    }
}
