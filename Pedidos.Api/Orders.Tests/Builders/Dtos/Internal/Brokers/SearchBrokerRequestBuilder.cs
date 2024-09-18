using Orders.Borders.Dtos.Internal.Brokers;

namespace Orders.Tests.Builders.Dtos.Internal.Brokers
{
    public class SearchBrokerRequestBuilder
    {
        private SearchBrokerRequest instance;

        public SearchBrokerRequestBuilder()
        {
            instance = new SearchBrokerRequest()
            {
                ProductId = Guid.NewGuid(),
                Name = "test",
                PageNumber = 1,
                PageSize = 10
            };
        }

        public SearchBrokerRequestBuilder WithProductId(Guid productId)
        {
            instance = instance with { ProductId = productId };
            return this;
        }
        public SearchBrokerRequestBuilder WithName(string name)
        {
            instance = instance with { Name = name };
            return this;
        }

        public SearchBrokerRequestBuilder WithPageNumber(int pageNumber)
        {
            instance = instance with { PageNumber = pageNumber };
            return this;
        }

        public SearchBrokerRequestBuilder WithPageSize(int pageSize)
        {
            instance = instance with { PageSize = pageSize };
            return this;
        }


        public SearchBrokerRequest Build() => instance;
    }
}
