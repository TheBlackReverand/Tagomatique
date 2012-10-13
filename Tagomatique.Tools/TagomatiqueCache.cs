using System;
using System.Collections;
using System.Collections.Generic;

namespace Tagomatique.Tools
{
    public static class TagomatiqueCache
    {
        #region Fields

        private static readonly Dictionary<Type, object> _cache = new Dictionary<Type, object>();
        private static DateTime _creation = DateTime.Now;

        #endregion Fields

        #region Delegates

        /// <summary>
        /// Délégué pour la méthode de chargement d'un type donné
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public delegate List<T> LoadAllMethod<T>();

        #endregion Delegates

        #region Methods

        /// <summary>
        /// Renvoie tous les éléments du type passé en generic
        /// </summary>
        /// <typeparam name="T">Type à renvoyer</typeparam>
        /// <param name="loadAllMethod">Methode de chargement si les éléments ne sont pas encore dans le cache</param>
        /// <returns></returns>
        public static List<T> GetAll<T>(LoadAllMethod<T> loadAllMethod)
            where T : class
        {
            Type type = typeof(T);

            // On gère en une seule "transaction" la purge éventuelle, le contrôle de présence et l'extraction, sans quoi des erreurs telles que "clé déjà présente" ou à l'inverse "clé absente" peuvent survenir exceptionnellement, notamment lors des purges pour la seconde erreur.
            lock (((ICollection)_cache).SyncRoot)
            {
                GererExpiration();

                if (!_cache.ContainsKey(type))
                {
                    List<T> liste = loadAllMethod();
                    _cache.Add(type, liste);
                }

                List<T> listeCache = _cache[type] as List<T>;
                return listeCache;
            }
        }

        /// <summary>
        /// Extrait du cache l'élément répondant au prédicat transmis.
        /// </summary>
        /// <typeparam name="T">Type d'élément</typeparam>
        /// <param name="selector">Prédicat de sélection</param>
        /// <param name="loadAllMethod">Méthode de chargement de tous les objets, qui sera appelée si le cache n'est pas déjà initialisé</param>
        /// <returns></returns>
        public static T GetElement<T>(Predicate<T> selector, LoadAllMethod<T> loadAllMethod)
            where T : class
        {
            return GetElement(selector, loadAllMethod, false);
        }

        /// <summary>
        /// Extrait du cache l'élément répondant au prédicat transmis.
        /// </summary>
        /// <typeparam name="T">Type d'élément</typeparam>
        /// <param name="selector">Prédicat de sélection</param>
        /// <param name="loadAllMethod">Méthode de chargement de tous les objets, qui sera appelée si le cache n'est pas déjà initialisé</param>
        /// <param name="forceReloadIfMissing">Force le rechargement si un élément n'est pas trouvé</param>
        /// <returns></returns>
        public static T GetElement<T>(Predicate<T> selector, LoadAllMethod<T> loadAllMethod, bool forceReloadIfMissing)
            where T : class
        {
            List<T> listeCache = GetAll(loadAllMethod);

            T element = listeCache.Find(selector);

            // Si l'élément a été trouvé ou si on ne doit pas relancer la recherche en cas d'absence de l'élément, on retourne la valeur trouvée
            if (element != null || !forceReloadIfMissing)
            {
                return element;
            }
            // Sinon on marque la collection liée à l'élément comme expirée et on rappelle une fois la recherche
            else
            {
                MarkAsDirty<T>();
                return GetElement(selector, loadAllMethod, false);
            }
        }

        ///<summary>
        /// Supprime la collection du cache si elle y existe, afin d'en forcer le rechargement
        ///</summary>
        ///<typeparam name="T">Type d'élément</typeparam>
        public static void MarkAsDirty<T>()
            where T : class
        {
            Type type = typeof(T);

            lock (_cache)
            {
                if (_cache.ContainsKey(type))
                {
                    _cache.Remove(type);
                }
            }
        }

        private static void GererExpiration()
        {
            // Réinitialisation du cache s'il a été initialisé il y a plus de 1h
            if (_creation < DateTime.Now.AddHours(-1))
            {
                _cache.Clear(); // Pour que SyncRoot puisse fonctionner il faut faire un Clear plutôt que créer une nouvelle instance.
                _creation = DateTime.Now;
            }
        }

        #endregion Methods
    }
}