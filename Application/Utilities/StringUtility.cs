namespace Application.Utilities;

public static class StringUtility
{
    public static string[] GetDataLines(string content)
    {
        return content.Split(["],", "], "], StringSplitOptions.RemoveEmptyEntries);
    }

    public static string[] GetValuesFromLine(string line)
    {
        var trimmedLine = line.Trim().Trim('[', ']');
        return trimmedLine.Split(',');
    }

    public static decimal ConvertFloatToDecimal(string value)
    {
        return (decimal)Convert.ToDouble(value.Replace(".", ","));
    }
}