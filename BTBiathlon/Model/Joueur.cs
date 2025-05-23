﻿namespace BTBiathlon.Model;

public class Joueur
{
    private int id;
    private int vie;
    private int scoreDefense;
    private int scoreAttaque;
    private int scoreSavoir; 
    
    
    public int Id { get => id; set => id = value; }
    
    public int Vie { get => vie; set => vie = value; }
    
    public int ScoreDefense { get => scoreDefense; set => scoreDefense = value; }
    
    public int ScoreAttaque { get => scoreAttaque; set => scoreAttaque = value; }
    
    public int ScoreSavoir { get => scoreSavoir; set => scoreSavoir = value; }

    public override bool Equals(object? obj)
    {
        return obj is Joueur joueur 
               && this.Vie == joueur.Vie
               && this.ScoreDefense == joueur.ScoreDefense
               && this.ScoreAttaque == joueur.ScoreAttaque
               && this.ScoreSavoir == joueur.ScoreSavoir;
    }
}