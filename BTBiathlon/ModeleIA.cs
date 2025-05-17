using Interface_communication;

namespace BTBiathlon;

public abstract class ModeleIA : IntelligenceArtificielle
{
    protected TypePhaseEnum GetPhase(int phase)
    {
        int phaseServeur = phase+1;
        var phaseEnum = TypePhaseEnum.Jour; // Si c'est pas la nuit ou la nuit de sang, c'est le jour
        if (phaseServeur == 17) // 17ème phase -> nuit de sang
        {
            phaseEnum = TypePhaseEnum.NuitSang;
        }
        else if (phaseServeur % 4 == 0) // Une phase sur 4 est une phase de nuit
        {
            phaseEnum = TypePhaseEnum.Nuit;
        }
        return phaseEnum;
    }
    
    public override List<Message> GetProtocoleDemarragePartie()
    {
        return [new Message("Bêta-Testeurs")];
    }
}