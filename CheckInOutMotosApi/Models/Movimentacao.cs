namespace CheckInOutMotosApi.Models
{
    public class Movimentacao
    {
        public int Id { get; set; }
        public int MotoId { get; set; }
        public Moto Moto { get; set; }
        public int PatioId { get; set; }
        public Patio Patio { get; set; }
        public DateTime DataHora { get; set; }
        public string Tipo { get; set; }
        public string Responsavel { get; set; }
    }
}