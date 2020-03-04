using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApp22
{
    public class IntervalTimer : IDisposable
    {
        /// <summary>
        /// ﾀｲﾏｰｲﾍﾞﾝﾄで実行する処理
        /// </summary>
        private List<Func<Task>> Tasks = new List<Func<Task>>();

        /// <summary>
        /// ﾀｲﾏｰを開始してからの累計時間
        /// </summary>
        private DateTime _startedtime;

        /// <summary>
        /// ﾀｲﾏｰ
        /// </summary>
        private Timer Timer;

        /// <summary>
        /// 処理中ﾌﾗｸﾞ
        /// </summary>
        private bool isCompleted = true;

        /// <summary>
        /// ﾀｲﾏｰ実行間隔
        /// </summary>
        public TimeSpan Interval { get; set; }

        /// <summary>
        /// 処理を非同期で実行する場合 true、追加順に実行する場合 false を設定します。
        /// </summary>
        public bool IsAsynchronous { get; set; } = true;

        /// <summary>
        /// ｺﾝｽﾄﾗｸﾀ
        /// </summary>
        public IntervalTimer()
        {
            Timer = new Timer();
            Timer.Interval = 1;
            Timer.Elapsed += async (sender, e) =>
            {
                // ﾀｲﾏｰを一時停止
                Timer.Stop();

                // 非同期処理をすべて実行する
                isCompleted = false;
                if (IsAsynchronous)
                {
                    await Task.WhenAll(Tasks.Select(task => task()));
                }
                else
                {
                    foreach (var task in Tasks)
                    {
                        await task();
                    }
                }
                isCompleted = true;

                // かかった時間だけ実行間隔を減らしてﾀｲﾏｰ再開
                var diff = (DateTime.Now - _startedtime).TotalMilliseconds;
                var next = Interval.TotalMilliseconds * Math.Ceiling(diff / Interval.TotalMilliseconds);
                Timer.Interval = next - diff;
                Timer.Start();
            };
        }

        /// <summary>
        /// ﾀｲﾏｰを開始します。
        /// </summary>
        public void Start()
        {
            Timer.Interval = 1;
            Timer.Start();
            _startedtime = DateTime.Now;
        }

        /// <summary>
        /// ﾀｲﾏｰを停止します。
        /// </summary>
        public void Stop()
        {
            Timer.Stop();
        }

        /// <summary>
        /// ﾀｲﾏｰｲﾍﾞﾝﾄで実行する処理を追加します。
        /// </summary>
        /// <param name="func">実行する処理(非同期)</param>
        public void Add(Func<Task> func)
        {
            Tasks.Add(func);
        }

        /// <summary>
        /// ﾀｲﾏｰｲﾍﾞﾝﾄで実行する処理を追加します。
        /// </summary>
        /// <param name="action">実行する処理(同期)</param>
        public void Add(Action action)
        {
            Add(() => Task.Run(action));
        }

        /// <summary>
        /// ﾀｲﾏｰｲﾍﾞﾝﾄで実行する処理を指定した位置に挿入します。
        /// </summary>
        /// <param name="index">挿入する位置</param>
        /// <param name="func">実行する処理(非同期)</param>
        public void Insert(int index, Func<Task> func)
        {
            Tasks.Insert(index, func);
        }

        /// <summary>
        /// ﾀｲﾏｰｲﾍﾞﾝﾄで実行する処理を指定した位置に挿入します。
        /// </summary>
        /// <param name="index">挿入する位置</param>
        /// <param name="action">実行する処理(同期)</param>
        public void Insert(int index, Action action)
        {
            Insert(index, () => Task.Run(action));
        }

        /// <summary>
        /// ﾀｲﾏｰｲﾍﾞﾝﾄで実行する処理をｸﾘｱします。
        /// </summary>
        public void Clear()
        {
            Tasks.Clear();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)。
                    while (!isCompleted)
                    {
                        System.Threading.Thread.Sleep(128);
                    }
                    Tasks.Clear();
                    Timer.Stop();
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~IntervalTimer() {
        //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        //   Dispose(false);
        // }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
            // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
