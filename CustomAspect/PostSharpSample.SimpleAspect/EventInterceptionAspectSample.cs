using PostSharp.Aspects;
using PostSharp.Serialization;
using System;

namespace PostSharpSample.SimpleAspect
{
    /// <summary>
    /// 攔截 Event 訂閱、取消訂閱
    /// 
    /// https://doc.postsharp.net/custompatterns/aspects/tutorials/event-interception
    /// </summary>
    public class EventInterceptionAspectSample
    {
        public void Main()
        {
            var eventSample = new EventSample();
            eventSample.SomeEvent += new EventHandler<EventArgs>((object sender, EventArgs e) => { Console.WriteLine("Run"); });
            //eventSample.SomeEvent -= new EventHandler<EventArgs>((object sender, EventArgs e) => { Console.WriteLine("Run"); });

            eventSample.DoSomething();
        }
    }

    public class EventSample
    {
        [CustomEventHandling]
        public event EventHandler<EventArgs> SomeEvent;

        public void DoSomething()
        {
            if (SomeEvent != null)
            {
                SomeEvent.Invoke(this, EventArgs.Empty);
            }
        }
    }

    [PSerializable]
    public class CustomEventHandling : EventInterceptionAspect
    {
        /// <summary>
        /// event 執行後
        /// </summary>
        public override void OnInvokeHandler(EventInterceptionArgs args)
        {
            base.OnInvokeHandler(args);
            Console.WriteLine("A handler was invoked");
        }

        /// <summary>
        /// event 訂閱事件
        /// </summary>
        public override void OnAddHandler(EventInterceptionArgs args)
        {
            base.OnAddHandler(args);
            Console.WriteLine("A handler was added");
        }

        /// <summary>
        /// event 取消訂閱事件
        /// </summary>
        public override void OnRemoveHandler(EventInterceptionArgs args)
        {
            base.OnRemoveHandler(args);
            Console.WriteLine("A handler was removed");
        }
    }
}