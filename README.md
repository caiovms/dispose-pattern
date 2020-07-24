# Resource release using the Dispose Pattern

It is quite common in today's projects to find a situation in which one works with different resources in memory. There are resources that are manageable and not manageable by the .NET virtual machine, the Common Language Runtime (CLR) which, after use, must free the memory occupied by these objects. When a managed object is no longer being used, the garbage collector automatically frees the memory allocated to it, but it is not possible to predict when the collection will occur. Non-manageable resources, such as connections to databases, files and external hardware, must be released manually.

# And how to implement?

It is necessary to implement the IDisposable interface to correctly release the resource, so that there is no risk of any user of this class accessing a resource that has already been released (in order not to throw the ObjectDisposedException exception).

It is advisable to call the Dispose method only at the place where the resource is to be cleaned.

The following are the approaches with which you can choose one or the other to free resources.


# Implementing the Dispose Pattern

You need to follow the steps below:

1. First, create a class that will implement the IDisposable interface;

2. Create a flag that will control whether the resource has already been released or not (bool disposed);

3. Create a Dispose method by receiving a boolean disposing which, depending on the value (if it is true will release the managed resources and, if false, the unmanaged resources). This method must be protected virtual because of the inheritance hierarchy. If you want, define derived classes that will need to release unmanaged resources after its use, in the subclass it must be overwritten. It is signed with protected to limit its visibility by other classes outside the hierarchy. Note: the base class (if any) must not have any finalizer.

4. Create a Dispose method without a parameter, as it will call the Dispose (true) method which, in fact, will perform the operations of releasing managed resources and will also make a call to the Garbage Collector's SuppressFinalize method, preventing it from calling the finalizer for that object. .

5. Finally, use the finalizer only if it was not implemented in your code inside a using or try-finally block with a call to Dispose.

```
class DisposePattern : IDisposable
{
    bool disposed = false;
  
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
  
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                //Dispose managed resources.   
            }

            //Dispose unmanaged resources.   

            disposed = true;
        }
    }
  
    ~DisposePattern()
    {
        Dispose(false);
    }
}

```
# Using

Using makes an implicit call to the Dispose method at the end of its scope. It is necessary for the class using Using to implement the IDisposable interface. It is recommended because it is a language resource, requiring no manual cleaning. The compiler automatically generates the try-finally blocks and the resource is instantiated within the Using statement.