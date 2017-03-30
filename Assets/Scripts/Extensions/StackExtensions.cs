using System.Collections.Generic;

namespace Assets.Scripts.Extensions
{
	public static class StackExtensions
	{
		public static void Repeat<T>(this Stack<T> self, T item, int count)
		{
			for (var i = 0; i < count; i++)
			{
				self.Push(item);
			}
		}
	}
}
