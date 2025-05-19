// Build a task scheduler
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Homework.Chapter1
{
    // Simple task priority enum
    public enum TaskPriority
    {
        Low,
        Normal,
        High
    }
    
    // Interface for task definition
    public interface IScheduledTask
    {
        string Name { get; }
        TaskPriority Priority { get; }
        TimeSpan Interval { get; }
        DateTime LastRun { get; }
        Task ExecuteAsync();
    }
    
    // A basic implementation of a scheduled task
    public class SimpleTask : IScheduledTask
    {
        private readonly Func<Task> _action;
        private DateTime _lastRun = DateTime.MinValue;
        
        public string Name { get; }
        public TaskPriority Priority { get; }
        public TimeSpan Interval { get; }
        
        public DateTime LastRun => _lastRun;
        
        public SimpleTask(string name, TaskPriority priority, TimeSpan interval, Func<Task> action)
        {
            Name = name;
            Priority = priority;
            Interval = interval;
            _action = action;
        }
        
        public async Task ExecuteAsync()
        {
            await _action();
            _lastRun = DateTime.Now;
        }
    }
    
    // The scheduler that students need to implement
    public class TaskScheduler
    {
        // TODO: Implement task queue/storage mechanism
        private readonly List<IScheduledTask> _tasks = new List<IScheduledTask>();
        public TaskScheduler()
        {
            // TODO: Initialize your scheduler
        }
        
        public void AddTask(IScheduledTask task)
        {
            // TODO: Add task to the scheduler
            if (_tasks.Exists(t => t.Name == task.Name))
            {
                throw new InvalidOperationException($"Task with name {task.Name} already exists.");
            }
            _tasks.Add(task);
        }
        
        public void RemoveTask(string taskName)
        {
            // TODO: Remove task from the scheduler
            if (!_tasks.Exists(t => t.Name == taskName))
            {
                throw new InvalidOperationException($"Task with name {taskName} not found.");
            }
            _tasks.RemoveAll(t => t.Name == taskName);
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // TODO: Implement the scheduling logic
            // - Run higher priority tasks first
            // - Only run tasks when their interval has elapsed since LastRun
            // - Keep running until cancellation is requested
            while (!cancellationToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                foreach (var task in _tasks
                             .OrderByDescending(t => t.Priority))
                {
                    if ((now - task.LastRun) >= task.Interval)
                    {
                        await task.ExecuteAsync();
                    }
                }
                await Task.Delay(200, cancellationToken); // Small delay to avoid busy loop
            }
        }
        
        public List<IScheduledTask> GetScheduledTasks()
        {
            // TODO: Return a list of all scheduled tasks
            return new List<IScheduledTask>(_tasks);
        }
    }
    
    class Hw3
    {
        public static async Task Run(string[] args)
        {
            Console.WriteLine("Task Scheduler Demo");
            
            // Create the scheduler
            var scheduler = new TaskScheduler();
            
            // Add some tasks
            scheduler.AddTask(new SimpleTask(
                "High Priority Task", 
                TaskPriority.High,
                TimeSpan.FromSeconds(2),
                async () => {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Running high priority task");
                    await Task.Delay(500); // Simulate some work
                }
            ));
            
            scheduler.AddTask(new SimpleTask(
                "Normal Priority Task", 
                TaskPriority.Normal,
                TimeSpan.FromSeconds(3),
                async () => {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Running normal priority task");
                    await Task.Delay(300); // Simulate some work
                }
            ));
            
            scheduler.AddTask(new SimpleTask(
                "Low Priority Task", 
                TaskPriority.Low,
                TimeSpan.FromSeconds(4),
                async () => {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Running low priority task");
                    await Task.Delay(200); // Simulate some work
                }
            ));
            
            // Create a cancellation token that will cancel after 20 seconds
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
            
            // Or allow the user to cancel with a key press
            Console.WriteLine("Press any key to stop the scheduler...");
            
            // Run the scheduler in the background
            var schedulerTask = scheduler.StartAsync(cts.Token);
            
            // Wait for user input
            Console.ReadKey();
            cts.Cancel();
            
            try
            {
                await schedulerTask;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Scheduler stopped by cancellation.");
            }
            
            Console.WriteLine("Scheduler demo finished!");
        }
    }
}