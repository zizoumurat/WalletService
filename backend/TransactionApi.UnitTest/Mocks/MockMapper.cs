using AutoMapper;
using Moq;

namespace TransactionApi.UnitTest.Mocks;

public class MockMapper
{
    public Mock<IMapper> Mapper { get; }

    public MockMapper()
    {
        Mapper = new Mock<IMapper>();
    }

}
