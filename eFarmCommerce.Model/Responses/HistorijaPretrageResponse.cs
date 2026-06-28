using System;

namespace eFarmCommerce.Model.Responses;

public class HistorijaPretrageResponse
{
    public int HistorijaPretrageId { get; set; }
    public int KorisnikId { get; set; }
    public string Pojam { get; set; } = null!;
    public string Tip { get; set; } = null!;
    public DateTime DatumPretrage { get; set; }
}