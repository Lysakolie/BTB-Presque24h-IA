using Interface_communication;

namespace BTBiathlon;

public abstract class ModeleIA : IntelligenceArtificielle
{
    protected TypePhaseEnum GetPhase(int phase)
    {
        var phaseEnum = TypePhaseEnum.Jour; // Si c'est pas la nuit ou la nuit de sang, c'est le jour
        if (phase == 16) // 17ème phase -> nuit de sang
        {
            phaseEnum = TypePhaseEnum.NuitSang;
        }
        else if (phase % 4 == 0) // Une phase sur 4 est une phase de nuit
        {
            phaseEnum = TypePhaseEnum.Nuit;
        }
        return phaseEnum;
    }
}