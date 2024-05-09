using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Base.Observer
{
    [ExecuteAlways]
    public static class ObserverManager 
    {

        private static Dictionary<Type, List<IEventListenerBase>> _subscribersList;

        static ObserverManager()
        {
            _subscribersList = new Dictionary<Type, List<IEventListenerBase>>();
        }

	    public static void AddListener<MKEvent>(EventListener<MKEvent> listener) where MKEvent : struct
        {
            Type eventType = typeof(MKEvent);

            if (!_subscribersList.ContainsKey(eventType))
                _subscribersList[eventType] = new List<IEventListenerBase>();

            if (!SubscriptionExists(eventType, listener))
                _subscribersList[eventType].Add(listener);
        }
	    public static void RemoveListener<MKEvent>(EventListener<MKEvent> listener) where MKEvent : struct
        {
            Type eventType = typeof(MKEvent);

            if (!_subscribersList.ContainsKey(eventType))
            {
#if EVENTROUTER_THROWEXCEPTIONS
					throw new ArgumentException( string.Format( "Removing listener \"{0}\", but the event type \"{1}\" isn't registered.", listener, eventType.ToString() ) );
#else
                return;
#endif
            }

            List<IEventListenerBase> subscriberList = _subscribersList[eventType];

#if EVENTROUTER_THROWEXCEPTIONS
	            bool listenerFound = false;
#endif

            for (int i = 0; i < subscriberList.Count; i++)
            {
                if (subscriberList[i] == listener)
                {
                    subscriberList.Remove(subscriberList[i]);
#if EVENTROUTER_THROWEXCEPTIONS
					    listenerFound = true;
#endif

                    if (subscriberList.Count == 0)
                    {
                        _subscribersList.Remove(eventType);
                    }

                    return;
                }
            }

#if EVENTROUTER_THROWEXCEPTIONS
		        if( !listenerFound )
		        {
					throw new ArgumentException( string.Format( "Removing listener, but the supplied receiver isn't subscribed to event type \"{0}\".", eventType.ToString() ) );
		        }
#endif
        }

        public static void TriggerEvent<MKEvent>(MKEvent newEvent) where MKEvent : struct
        {
            List<IEventListenerBase> list;
            if (!_subscribersList.TryGetValue(typeof(MKEvent), out list))
#if EVENTROUTER_REQUIRELISTENER
			            throw new ArgumentException( string.Format( "Attempting to send event of type \"{0}\", but no listener for this type has been found. Make sure this.Subscribe<{0}>(EventRouter) has been called, or that all listeners to this event haven't been unsubscribed.", typeof( MMEvent ).ToString() ) );
#else
                return;
#endif
            List<IEventListenerBase> listTemp = list.ToList();
            for (int i = 0; i < listTemp.Count; i++)
            {
                (listTemp[i] as EventListener<MKEvent>).OnMMEvent(newEvent);
            }
        }
        private static bool SubscriptionExists(Type type, IEventListenerBase receiver)
        {
            List<IEventListenerBase> receivers;

            if (!_subscribersList.TryGetValue(type, out receivers)) return false;

            bool exists = false;

            for (int i = 0; i < receivers.Count; i++)
            {
                if (receivers[i] == receiver)
                {
                    exists = true;
                    break;
                }
            }

            return exists;
        }

    }
	public static class EventRegister
    {
        public delegate void Delegate<T>(T eventType);

        public static void ObserverStartListening<EventType>(this EventListener<EventType> caller) where EventType : struct
        {
            ObserverManager.AddListener<EventType>(caller);
        }

        public static void ObserverStopListening<EventType>(this EventListener<EventType> caller) where EventType : struct
        {
            ObserverManager.RemoveListener<EventType>(caller);
        }
    }

}
