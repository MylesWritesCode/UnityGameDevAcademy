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

// Data Encapsulation is keeping the data in the class and not be accessible from the outside. This keeps the program clean when classes talk to each other.
// ie. If you work at FedEx and you change a process on delivering packages, your customers may not know about the process change since it doesn't affect them.

// Abstraction is when you call on a class and expect a result without the calling class knowing the inner workings of the abstracted class.
// ie. If you are a business and you need to ship a package, you give a company like FedEx the package and pass in parameters such as delivered by date, method of shipping, weight, etc.

// A Singleton ensures that a class has only one instance active in the program and allows other classes to access it through abstraction. 
// ie. If you are running a Fedex, you want to make sure that there's only one main office in any given area. Having two main FedEx locations too close to each other (on the same street, for example) is inefficient and may cause problems, such as operational problems or you lose a lot of money for having too many instances of a main office in a given area.

// ...so this means: Singleton.cs is an abstracted class that makes GameManager and TowerManager singletons. This allows there to only be one instance of both GameManager and TowerManager, and they control things such as the game or the towers. 