namespace StockSharp.Algo;

/// <summary>
/// Subscription provider interface.
/// </summary>
public interface ISubscriptionProvider
{
	/// <summary>
	/// Subscriptions.
	/// </summary>
	IEnumerable<Subscription> Subscriptions { get; }

	/// <summary>
	/// Message received.
	/// </summary>
	event Action<Subscription, object> SubscriptionReceived;

	/// <summary>
	/// Level1 received.
	/// </summary>
	event Action<Subscription, Level1ChangeMessage> Level1Received;

	/// <summary>
	/// Order book received.
	/// </summary>
	event Action<Subscription, IOrderBookMessage> OrderBookReceived;

	/// <summary>
	/// Tick trade received.
	/// </summary>
	event Action<Subscription, ITickTradeMessage> TickTradeReceived;

	/// <summary>
	/// Tick trade received.
	/// </summary>
	event Action<Subscription, IOrderLogMessage> OrderLogReceived;

	/// <summary>
	/// Security received.
	/// </summary>
	event Action<Subscription, Security> SecurityReceived;

	/// <summary>
	/// Board received.
	/// </summary>
	event Action<Subscription, ExchangeBoard> BoardReceived;

	/// <summary>
	/// Order book received.
	/// </summary>
	[Obsolete("Use OrderBookReceived event.")]
	event Action<Subscription, MarketDepth> MarketDepthReceived;

	/// <summary>
	/// Order log received.
	/// </summary>
	[Obsolete("Use OrderLogReceived event.")]
	event Action<Subscription, OrderLogItem> OrderLogItemReceived;

	/// <summary>
	/// News received.
	/// </summary>
	event Action<Subscription, News> NewsReceived;

	/// <summary>
	/// Candle received.
	/// </summary>
	event Action<Subscription, ICandleMessage> CandleReceived;

	/// <summary>
	/// Own trade received.
	/// </summary>
	event Action<Subscription, MyTrade> OwnTradeReceived;

	/// <summary>
	/// Order received.
	/// </summary>
	event Action<Subscription, Order> OrderReceived;

	/// <summary>
	/// Order registration error event.
	/// </summary>
	event Action<Subscription, OrderFail> OrderRegisterFailReceived;

	/// <summary>
	/// Order cancellation error event.
	/// </summary>
	event Action<Subscription, OrderFail> OrderCancelFailReceived;

	/// <summary>
	/// Order edition error event.
	/// </summary>
	event Action<Subscription, OrderFail> OrderEditFailReceived;

	/// <summary>
	/// Portfolio received.
	/// </summary>
	event Action<Subscription, Portfolio> PortfolioReceived;

	/// <summary>
	/// Position received.
	/// </summary>
	event Action<Subscription, Position> PositionReceived;

	/// <summary>
	/// Subscription is online.
	/// </summary>
	event Action<Subscription> SubscriptionOnline;

	/// <summary>
	/// Subscription is started.
	/// </summary>
	event Action<Subscription> SubscriptionStarted;

	/// <summary>
	/// Subscription is stopped.
	/// </summary>
	event Action<Subscription, Exception> SubscriptionStopped;

	/// <summary>
	/// Subscription is failed.
	/// </summary>
	event Action<Subscription, Exception, bool> SubscriptionFailed;

	/// <summary>
	/// Subscribe.
	/// </summary>
	/// <param name="subscription">Subscription.</param>
	void Subscribe(Subscription subscription);

	/// <summary>
	/// Unsubscribe.
	/// </summary>
	/// <param name="subscription">Subscription.</param>
	void UnSubscribe(Subscription subscription);
}