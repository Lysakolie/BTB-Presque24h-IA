using BTBiathlon;
using Interface_communication.Utils;
using Interface_communication.Utils.Logging;

//Config
Config.NombrePhaseTour= 17;//Potentiellement faire une énum ?
Logger.NiveauLog = NiveauxLog.Info;

//IA
IALogique drunken = new IALogique();
drunken.Jouer();