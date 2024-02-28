

namespace Stories.API.Application.Queries.Responses
{
    public class FindStoryResponse
    {
        public FindStoryResponse(int id, string title, string description, string departament, ICollection<VoteResponse> votes)
        {
            Id = id;
            Title = title;
            Description = description;
            Departament = departament;
            Votes = votes;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Departament { get; set; }
        public ICollection<VoteResponse> Votes { get; set; }
    }
}
