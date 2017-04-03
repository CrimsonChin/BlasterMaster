using System;
using System.Collections.Generic;
using System.Linq;

public static class KnuthShuffleExtensions
{
	private static readonly Random Rand = new Random();

	/// <summary>
	/// Knuth shuffle algorithm
	/// </summary>
	public static void KnuthShuffle<T>(this T[] items)
	{

		for (var currentIndex = 0; currentIndex < items.Length; currentIndex++)
		{
			var currentItem = items[currentIndex];
			var randomIndex = Rand.Next(currentIndex, items.Length);
			items[currentIndex] = items[randomIndex];
			items[randomIndex] = currentItem;
		}
	}

	/// <summary>
	/// Knuth shuffle algorithm
	/// </summary>
	public static IList<T> KnuthShuffle<T>(this IList<T> items)
	{
		for (var currentIndex = 0; currentIndex < items.Count; currentIndex++)
		{
			var currentItem = items[currentIndex];
			var randomIndex = Rand.Next(currentIndex, items.Count);
			items[currentIndex] = items[randomIndex];
			items[randomIndex] = currentItem;
		}

		return items;
	}
}
