using TransactionApi.UnitTest.Mocks;

namespace TransactionApi.UnitTest.Fixture;

public class MockFixture
{
    public MockRepository MockRepository { get; }
    public MockMapper MockMapper { get; }

    public MockFixture()
    {
        MockRepository = new MockRepository();
        MockMapper = new MockMapper();
    }
}
