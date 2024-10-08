﻿using System;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Strategies;
using StockSharp.Messages;
using StockSharp.BusinessEntities;

namespace StockSharp.Samples.Strategies.HistoryTrend
{
	public class StairsCountertrendStrategy : Strategy
	{
		private readonly CandleSeries _candleSeries;
		private Subscription _subscription;

		public StairsCountertrendStrategy(CandleSeries candleSeries)
		{
			_candleSeries = candleSeries;
		}

		private int _bullLength;
		private int _bearLength;
		private int Length { get; set; } = 3;
		protected override void OnStarted(DateTimeOffset time)
		{
			CandleReceived += OnCandleReceived;
			_subscription = this.SubscribeCandles(_candleSeries);

			base.OnStarted(time);
		}

		protected override void OnStopped()
		{
			if (_subscription != null)
			{
				UnSubscribe(_subscription);
				_subscription = null;
			}

			base.OnStopped();
		}

		private void OnCandleReceived(Subscription subscription, ICandleMessage candle)
		{
			if (subscription != _subscription)
				return;

			if (candle.State != CandleStates.Finished) return;

			if (candle.OpenPrice < candle.ClosePrice)
			{
				_bullLength++;
				_bearLength = 0;
			}
			else
			if (candle.OpenPrice > candle.ClosePrice)
			{
				_bullLength = 0;
				_bearLength++;
			}

			if (_bullLength >= Length && Position >= 0)
			{
				SellMarket(Volume + Math.Abs(Position));
			}

			else
			if (_bearLength >= Length && Position <= 0)
			{
				BuyMarket(Volume + Math.Abs(Position));
			}
		}
	}
}