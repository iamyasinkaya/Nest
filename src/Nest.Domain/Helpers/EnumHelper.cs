namespace Nest.Domain;

public static class EnumHelper
{
    public static string GetLocationNameFromCode(int locationCode)
    {
        // Enum'daki değerleri kontrol ederek, int kodun karşılığını buluyoruz
        if (Enum.IsDefined(typeof(TurkishCities), locationCode))
        {
            return ((TurkishCities)locationCode).ToString();
        }

        return string.Empty; // Kod bulunamazsa boş döner
    }
}
