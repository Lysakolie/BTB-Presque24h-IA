namespace BTBiathlon.Model;

public class Carte
{
    private int valeur;
    private TypeCarteEnum type;
    
    public Carte(int valeur, TypeCarteEnum type)
    {
        this.valeur = valeur;
        this.type = type;
    }

    public int Valeur => valeur;
    
    public TypeCarteEnum Type => type;
}