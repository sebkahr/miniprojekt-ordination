namespace shared.Model;

public class PN : Ordination {
	public double antalEnheder { get; set; }
    public List<Dato> dates { get; set; } = new List<Dato>();

    public PN (DateTime startDen, DateTime slutDen, double antalEnheder, Laegemiddel laegemiddel) : base(laegemiddel, startDen, slutDen) {
		this.antalEnheder = antalEnheder;
	}

    public PN() : base(null!, new DateTime(), new DateTime()) {
    }

    /// <summary>
    /// Registrerer at der er givet en dosis på dagen givesDen
    /// Returnerer true hvis givesDen er inden for ordinationens gyldighedsperiode og datoen huskes
    /// Returner false ellers og datoen givesDen ignoreres
    /// </summary>
    
    // Gemmer datoen og returnerer true, hvis dosis er givet inden for ordinationens periode (begge dage medregnet). Ellers false.
    public bool givDosis(Dato givesDen) {
        DateTime dag = givesDen.dato.Date;
        if (dag >= startDen.Date && dag <= slutDen.Date) {
            dates.Add(givesDen);
            return true;
        }
        return false;
    }

    // Gennemsnitlig dosis pr. dag: samlet dosis delt med antal dage fra første til sidste givning. Returnerer 0, hvis der ikke er givet nogen doser.
    public override double doegnDosis() {
        if (dates.Count == 0) {
            return 0;
        }

        DateTime foerste = dates.Min(d => d.dato.Date);
        DateTime sidste = dates.Max(d => d.dato.Date);
        int antalDage = (sidste - foerste).Days + 1;

        return samletDosis() / antalDage;
    }


    public override double samletDosis() {
        return dates.Count() * antalEnheder;
    }

    public int getAntalGangeGivet() {
        return dates.Count();
    }

	public override String getType() {
		return "PN";
	}
}
