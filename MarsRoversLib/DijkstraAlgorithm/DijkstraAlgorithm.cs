﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversLib.DijkstraAlgorithm
{
    public class Path<T>
    {

        public T Source { get; set; }

        public T Destination { get; set; }

        public int Cost { get; set; }

    }

    public static class Engine
    {
        public static LinkedList<Path<T>> CalculateShortestPathBetween<T>(T source, T destination, IEnumerable<Path<T>> Paths)
        {
            return CalculateFrom(source, Paths)[destination];
        }

        public static Dictionary<T, LinkedList<Path<T>>> CalculateShortestFrom<T>(T source, IEnumerable<Path<T>> Paths)
        {
            return CalculateFrom(source, Paths);
        }

        private static Dictionary<T, LinkedList<Path<T>>> CalculateFrom<T>(T source, IEnumerable<Path<T>> Paths)
        {
            // validate the paths

            if (Paths.Any(p => p.Source.Equals(p.Destination)))

                throw new ArgumentException("No path can have the same source and destination");

            // keep track of the shortest paths identified thus far

            Dictionary<T, KeyValuePair<int, LinkedList<Path<T>>>> ShortestPaths = new Dictionary<T, KeyValuePair<int, LinkedList<Path<T>>>>();

            // keep track of the locations which have been completely processed

            List<T> LocationsProcessed = new List<T>();

            // include all possible steps, with Int.MaxValue cost

            Paths.SelectMany(p => new T[] { p.Source, p.Destination })              // union source and destinations
                 .Distinct()                                                        // remove duplicates
                 .ToList()                                                          // ToList exposes ForEach
                 .ForEach(s => ShortestPaths.Set(s, Int32.MaxValue, null));         // add to ShortestPaths with MaxValue cost

            // update cost for self-to-self as 0; no path

            ShortestPaths.Set(source, 0, null);

            // keep this cached

            var LocationCount = ShortestPaths.Keys.Count;

            while (LocationsProcessed.Count < LocationCount)
            {
                T _locationToProcess = default(T);
                //Search for the nearest location that isn't handled

                foreach (T _location in ShortestPaths.OrderBy(p => p.Value.Key).Select(p => p.Key).ToList())
                {
                    if (!LocationsProcessed.Contains(_location))
                    {
                        if (ShortestPaths[_location].Key == Int32.MaxValue)

                            return ShortestPaths.ToDictionary(k => k.Key, v => v.Value.Value); //ShortestPaths[destination].Value;

                        _locationToProcess = _location;

                        break;
                    }

                }

                var _selectedPaths = Paths.Where(p => p.Source.Equals(_locationToProcess));

                foreach (Path<T> path in _selectedPaths)
                {

                    if (ShortestPaths[path.Destination].Key > path.Cost + ShortestPaths[path.Source].Key)
                    {
                        ShortestPaths.Set(
                            path.Destination,
                            path.Cost + ShortestPaths[path.Source].Key,
                            ShortestPaths[path.Source].Value.Union(new Path<T>[] { path }).ToArray());
                    }

                }

                //Add the location to the list of processed locations

                LocationsProcessed.Add(_locationToProcess);
            }

            return ShortestPaths.ToDictionary(k => k.Key, v => v.Value.Value);

            //return ShortestPaths[destination].Value;

        }

    } // class Engine

    public static class ExtensionMethods
    {

        /// <summary>

        /// Adds or Updates the dictionary to include the destination and its associated cost and complete path (and param arrays make paths easier to work with)

        /// </summary>

        public static void Set<T>(this Dictionary<T, KeyValuePair<int, LinkedList<Path<T>>>> Dictionary, T destination, int Cost, params Path<T>[] paths)
        {

            var CompletePath = paths == null ? new LinkedList<Path<T>>() : new LinkedList<Path<T>>(paths);

            Dictionary[destination] = new KeyValuePair<int, LinkedList<Path<T>>>(Cost, CompletePath);

        }

    }
}
