using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Puffix.Cqrs.Rest.Helpers;

/// <summary>
/// Class utilitaire pour faciliter l'extraction de données JSON.
/// </summary>
public static class JsonContentExtractor
{
    /// <summary>
    /// Extraction d'un énumérateur d'éléments JSON.
    /// </summary>
    /// <param name="jsonElement">Element JSON</param>
    /// <param name="propertyName">Nom de la propriété.</param>
    /// <param name="jsonElementCollection">Enumérateur d'éléments JSON</param>
    /// <returns>Indique si les données ont été extraites.</returns>
    public static bool TryExtractJsonArray(this JsonElement jsonElement, string propertyName, out IEnumerable<JsonElement> jsonElementCollection)
    {
        bool isValidValue;
        if (jsonElement.ValueKind != JsonValueKind.Undefined &&
            jsonElement.TryGetProperty(propertyName, out JsonElement arrayJsonElement) &&
            arrayJsonElement.ValueKind == JsonValueKind.Array &&
            arrayJsonElement.EnumerateArray().Any())
        {
            jsonElementCollection = arrayJsonElement.EnumerateArray();
            isValidValue = true;
        }
        else
        {
            jsonElementCollection = default;
            isValidValue = false;
        }

        return isValidValue;
    }

    /// <summary>
    /// Extraction du premier élément d'un tableau JSON.
    /// </summary>
    /// <param name="jsonElement">Element JSON</param>
    /// <param name="propertyName">Nom de la propriété.</param>
    /// <param name="firstArrayJsonElement"></param>
    /// <returns>Indique si les données ont été extraites.</returns>
    public static bool TryExtractFirstJsonArrayElement(this JsonElement jsonElement, string propertyName, out JsonElement firstArrayJsonElement)
    {
        bool isValidValue;
        if (jsonElement.ValueKind != JsonValueKind.Undefined &&
            jsonElement.TryGetProperty(propertyName, out JsonElement arrayJsonElement) &&
            arrayJsonElement.ValueKind == JsonValueKind.Array &&
            arrayJsonElement.EnumerateArray().Any())
        {
            firstArrayJsonElement = arrayJsonElement.EnumerateArray().First();
            isValidValue = true;
        }
        else
        {
            firstArrayJsonElement = default;
            isValidValue = false;
        }

        return isValidValue;
    }

    /// <summary>
    /// Extraction d'un élément JSON.
    /// </summary>
    /// <param name="jsonElement">Element JSON</param>
    /// <param name="propertyName">Nom de la propriété.</param>
    /// <param name="extractedJsonElement"></param>
    /// <returns>Indique si les données ont été extraites.</returns>
    public static bool TryExtractJsonElement(this JsonElement jsonElement, string propertyName, out JsonElement extractedJsonElement)
    {
        bool isValidValue;
        if (jsonElement.ValueKind != JsonValueKind.Undefined &&
            jsonElement.TryGetProperty(propertyName, out extractedJsonElement) &&
            extractedJsonElement.ValueKind != JsonValueKind.Undefined)
        {
            isValidValue = true;
        }
        else
        {
            extractedJsonElement = default(JsonElement);
            isValidValue = false;
        }

        return isValidValue;
    }

    /// <summary>
    /// Extraction d'une valeur de type chaîne de caractères.
    /// </summary>
    /// <param name="jsonElement">Element JSON</param>
    /// <param name="propertyName">Nom de la propriété.</param>
    /// <param name="defaultValue">Valeur par défaut</param>
    /// <param name="extractedValue">Valeur extraite (prend la valeur par défaut si elle ne peut pas être extraite).</param>
    /// <returns>Indique si les données ont été extraites.</returns>
    public static bool TryExtractStringValueFromJsonElement(this JsonElement jsonElement, string propertyName, string defaultValue, out string extractedValue)
    {
        bool isValidValue;
        if (jsonElement.ValueKind != JsonValueKind.Undefined &&
            jsonElement.TryGetProperty(propertyName, out JsonElement jsonValueElement) &&
            jsonValueElement.ValueKind == JsonValueKind.String)
        {
            extractedValue = jsonValueElement.GetString() ?? defaultValue;
            isValidValue = true;
        }
        else
        {
            extractedValue = defaultValue;
            isValidValue = false;
        }

        return isValidValue;
    }

    /// <summary>
    /// Extraction d'une valeur de type booléen.
    /// </summary>
    /// <param name="jsonElement">Element JSON</param>
    /// <param name="propertyName">Nom de la propriété.</param>
    /// <param name="defaultValue">Valeur par défaut</param>
    /// <param name="extractedValue">Valeur extraite (prend la valeur par défaut si elle ne peut pas être extraite).</param>
    /// <returns>Indique si les données ont été extraites.</returns>
    public static bool TryExtractBooleanValueFromJsonElement(this JsonElement jsonElement, string propertyName, bool defaultValue, out bool extractedValue)
    {
        bool isValidValue;
        if (jsonElement.ValueKind != JsonValueKind.Undefined &&
            jsonElement.TryGetProperty(propertyName, out JsonElement jsonValueElement) &&
            (jsonValueElement.ValueKind == JsonValueKind.True || jsonValueElement.ValueKind == JsonValueKind.False))
        {
            extractedValue = jsonValueElement.GetBoolean();
            isValidValue = true;
        }
        else
        {
            extractedValue = defaultValue;
            isValidValue = false;
        }

        return isValidValue;
    }

    /// <summary>
    /// Extraction d'une valeur de type entier.
    /// </summary>
    /// <param name="jsonElement">Element JSON</param>
    /// <param name="propertyName">Nom de la propriété.</param>
    /// <param name="defaultValue">Valeur par défaut</param>
    /// <param name="extractedValue">Valeur extraite (prend la valeur par défaut si elle ne peut pas être extraite).</param>
    /// <returns>Indique si les données ont été extraites.</returns>
    public static bool TryExtractIntegrerValueFromJsonElement(this JsonElement jsonElement, string propertyName, int defaultValue, out int extractedValue)
    {
        bool isValidValue;
        if (jsonElement.ValueKind != JsonValueKind.Undefined &&
            jsonElement.TryGetProperty(propertyName, out JsonElement jsonValueElement) &&
            jsonValueElement.ValueKind == JsonValueKind.Number &&
            jsonValueElement.TryGetInt32(out extractedValue))
        {
            isValidValue = true;
        }
        else
        {
            extractedValue = defaultValue;
            isValidValue = false;
        }

        return isValidValue;
    }

    /// <summary>
    /// Extraction d'une valeur de type entier long.
    /// </summary>
    /// <param name="jsonElement">Element JSON</param>
    /// <param name="propertyName">Nom de la propriété.</param>
    /// <param name="defaultValue">Valeur par défaut</param>
    /// <param name="extractedValue">Valeur extraite (prend la valeur par défaut si elle ne peut pas être extraite).</param>
    /// <returns>Indique si les données ont été extraites.</returns>
    public static bool TryExtractLongValueFromJsonElement(this JsonElement jsonElement, string propertyName, long defaultValue, out long extractedValue)
    {
        bool isValidValue;
        if (jsonElement.ValueKind != JsonValueKind.Undefined &&
            jsonElement.TryGetProperty(propertyName, out JsonElement jsonValueElement) &&
            jsonValueElement.ValueKind == JsonValueKind.Number &&
            jsonValueElement.TryGetInt64(out extractedValue))
        {
            isValidValue = true;
        }
        else
        {
            extractedValue = defaultValue;
            isValidValue = false;
        }

        return isValidValue;
    }

    /// <summary>
    /// Extraction d'une valeur de type double.
    /// </summary>
    /// <param name="jsonElement">Element JSON</param>
    /// <param name="propertyName">Nom de la propriété.</param>
    /// <param name="defaultValue">Valeur par défaut</param>
    /// <param name="extractedValue">Valeur extraite (prend la valeur par défaut si elle ne peut pas être extraite).</param>
    /// <returns>Indique si les données ont été extraites.</returns>
    public static bool TryExtractDoubleValueFromJsonElement(this JsonElement jsonElement, string propertyName, double defaultValue, out double extractedValue)
    {
        bool isValidValue;
        if (jsonElement.ValueKind != JsonValueKind.Undefined &&
            jsonElement.TryGetProperty(propertyName, out JsonElement jsonValueElement) &&
            jsonValueElement.ValueKind == JsonValueKind.Number &&
            jsonValueElement.TryGetDouble(out extractedValue))
        {
            isValidValue = true;
        }
        else
        {
            extractedValue = defaultValue;
            isValidValue = false;
        }

        return isValidValue;
    }
}
