namespace Shared.Respons
{

     public class FailureResponse
     {
         public string? Type { get; set; }
         public string? Title { get; set; }
         public int Status { get; set; }
         public string? Detail { get; set; }
         public List<FailureError> Errors { get; set; } = [];
     }
    
    public class FailureError
    {
        public string? Code { get; set; }
        public string? Message { get; set; }

    }
}
