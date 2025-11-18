using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandManager : Singleton<CommandManager>
{
    [Header("# Recording")]
    [SerializeField] private bool _isRecording;
    [SerializeField] private float _recordingTime;

    [Header("# Replaying")]
    [SerializeField] private bool _isReplaying;
    [SerializeField] private float _replayTime;
    
    private SortedList<float, ICommand> _recordCommandList = new();

    public void Execute(ICommand command)
    {
        command.Execute();
        
        if (_isRecording) 
            _recordCommandList.Add(_recordingTime, command);
        
        Debug.Log($"Recorded Time: {_recordingTime}");
    }

    public void Record()
    {
        _recordingTime = 0.0f;
        
        _isRecording = true;
        _isReplaying = false;
        
        _recordCommandList.Clear();
    }

    public void Replay()
    {
        _replayTime = 0.0f;
        _isRecording = false;

        if (_recordCommandList.Count == 0)
        {
            return;
        }
        
        _isReplaying = true;

        _recordCommandList.Reverse();
    }

    private void FixedUpdate()
    {
        OnRecording();
        OnReplaying();
    }

    private void OnRecording()
    {
        if (!_isRecording)
            return;
        
        _recordingTime += Time.fixedDeltaTime;
    }

    private void OnReplaying()
    {
        if (!_isReplaying)
            return;
        
        _replayTime += Time.fixedDeltaTime;

        if (_recordCommandList.Any())
        {
            if (!Mathf.Approximately(_replayTime, _recordCommandList.Keys[0])) 
                return;
            
            _recordCommandList.Values[0].Execute();
            _recordCommandList.RemoveAt(0);
        }

        else
        {
            _isReplaying = false;
        }
    }
}