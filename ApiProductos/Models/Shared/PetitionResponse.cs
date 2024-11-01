namespace ApiProductos.Models.Shared
{
    public class PetitionResponse
    {
        public bool Success { get; set; }
        public Object Result { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
    }
}
