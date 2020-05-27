using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseTest : MonoBehaviour
{
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;

    protected virtual void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log("Capable of starting firebase Connection...");
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    protected virtual void InitializeFirebase()
    {
        //This is how you get a reference point to the database
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Test2");

        //This is how you write a json to a value
        //Note the Original value will be overwritten of you dont add it in the update
        reference.SetRawJsonValueAsync("{\"Iets2\":\"Niets2\", \"NogIets\":\"nooit\"}").ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.Log("mislukt!");
            }
            else if (task.IsCompleted)
            {
                Debug.Log("transaction complete.");
            }
        });

        //This makes an unique identifier
        DatabaseReference reference2 = reference.Push(); 
        
        //This is how you query values
        reference.GetValueAsync().ContinueWithOnMainThread(task2 =>
        {
            DataSnapshot snapshot = task2.Result;
            Debug.Log(snapshot.Key);
            Debug.Log(snapshot.GetRawJsonValue().ToString());
        });

    }

    //This is how you make a listener. 
    protected void StartListener()
    {
        FirebaseDatabase.DefaultInstance
          .GetReference("Leaders").OrderByChild("score")
          .ValueChanged += (object sender2, ValueChangedEventArgs e2) => {
              if (e2.DatabaseError != null)
              {
                  Debug.LogError(e2.DatabaseError.Message);
                  return;
              }
              Debug.Log("Received values for Leaders.");
              if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0)
              {
                  foreach (var childSnapshot in e2.Snapshot.Children)
                  {
                      if (childSnapshot.Child("score") == null
                    || childSnapshot.Child("score").Value == null)
                      {
                          Debug.LogError("Bad data in sample.  Did you forget to call SetEditorDatabaseUrl with your project id?");
                          break;
                      }
                      else
                      {
                          Debug.Log("Leaders entry : " +
                        childSnapshot.Child("email").Value.ToString() + " - " +
                        childSnapshot.Child("score").Value.ToString());
                      }
                  }
              }
          };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
