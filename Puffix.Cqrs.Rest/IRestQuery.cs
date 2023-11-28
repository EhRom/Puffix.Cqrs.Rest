using Puffix.Cqrs.Context;
using Puffix.Cqrs.Queries;
using System;

namespace Puffix.Cqrs.Rest;

/// <summary>
/// Contrat de définition d'une requête REST.
/// </summary>
/// <typeparam name="ResultT">Type du résutlat de la reuqête REST.</typeparam>
/// <typeparam name="RestQueryResultT">Type du résultat brute de la requête REST.</typeparam>
public interface IRestQuery<ResultT, RestQueryResultT> : IQuery<ResultT>
{
    /// <summary>
    /// Construction de l'URI d'appel du service REST.
    /// </summary>
    /// <param name="executionContext">Contexte d'exécution.</param>
    /// <param name="applicationContext">Context de l'application.</param>
    /// <returns>URI d'appel.</returns>
    Uri BuildUri(IExecutionContext executionContext, IApplicationContext applicationContext);

    /// <summary>
    /// Extraction du résulat de la requête REST.
    /// </summary>
    /// <param name="queryResult">Résultat de la requête REST.</param>
    /// <returns>Résultat de la requête.</returns>
    ResultT ExtractResult(RestQueryResultT queryResult);
}
