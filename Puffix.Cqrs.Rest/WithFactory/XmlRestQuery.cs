using System;
using System.Net.Http;
using System.Xml.Linq;

namespace Puffix.Cqrs.Rest.WithFactory;

/// <summary>
/// Implémentation de base pour la définition d'une requête REST avec un résultat au format XML.
/// </summary>
/// <typeparam name="ResultT">Type du résutlat de la reuqête REST.</typeparam>
public abstract class XmlRestQuery<ResultT> : RestQuery<ResultT, XElement>
{
    #region Propriétés

    /// <summary>
    /// Interprétation du résultat de la requête.
    /// </summary>
    protected override Func<string, XElement> ParseResult => restResult => XElement.Parse(restResult);

    #endregion Propriétés

    /// <summary>
    /// Constructeur.
    /// </summary>
    /// <param name="httpClientFactory">Usine pour la gestion des clients HTTP.</param>
    public XmlRestQuery(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory)
    { }
}
