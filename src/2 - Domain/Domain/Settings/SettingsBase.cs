namespace Domain.Settings
{
    public abstract class SettingsBase
    {
        protected string MontarTextoChaveValor(string chave, string valor)
        {
            var str = !string.IsNullOrEmpty(valor) ? $"{chave}: {valor}" : $"{chave} não encontrado.";

            return str;
        }
    }
}
