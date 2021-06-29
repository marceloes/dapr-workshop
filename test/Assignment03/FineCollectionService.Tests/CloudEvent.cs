namespace FineCollectionService.Tests 
{
	public class CloudEvent<T> where T : class
    {
        public string id { get; set; }

        public string datacontenttype { get; set; }

        public string type { get; set; }

        public string specversion { get; set; }

        public string source { get; set; }

        public T data { get; set; }
				
				public string pubsubname { get; set; }

				public string topic { get; set; }
    }
}