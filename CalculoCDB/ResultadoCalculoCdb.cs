using Flunt.Notifications;

namespace CalculoCDB
{
    public class ResultadoCalculoCdb : Notifiable<Notification>
    {
        public decimal ValorInicial { get; set; }
        public DateOnly DataRetirada { get; set; }
        public decimal Porcentagem { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal ValorLiquido { get; set; }
    }
}
