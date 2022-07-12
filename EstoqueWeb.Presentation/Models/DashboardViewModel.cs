namespace EstoqueWeb.Presentation.Models
{
    public class DashboardViewModel
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public int TotalAtivos { get; set; }
        public int TotalInativos { get; set; }
    }
}
