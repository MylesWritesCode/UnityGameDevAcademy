// A small lesson on Generics.
using UnityEngine;

public class Loader : MonoBehaviour 
{
	void Awake()
	{
		KeyValuePair<int, string> course = new KeyValuePair<int, string>(4, "Myles");
		KeyValuePair<string, string> lesson = new KeyValuePair<string, string>("Lesson", "Generics");
		course.Print();
		lesson.Print();
	}
}

public class KeyValuePair<TKey, TValue>
{
	public TKey key;
	public TValue value;

	public KeyValuePair(TKey _key, TValue _value)
	{
		key = _key;
		value = _value;
	}

	public void Print()
	{
		Debug.Log("Key: " + key.ToString());
		Debug.Log("Value: " + value.ToString());
	}
}