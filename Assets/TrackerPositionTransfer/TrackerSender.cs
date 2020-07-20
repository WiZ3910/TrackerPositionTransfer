using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using MessagePack;
using UnityEditor;
using UnityEngine;

namespace TrackerPositionTransfer
{
    public class TrackerSender : MonoBehaviour
    {
        [SerializeField] private Transform[] trackers;
        private UdpClient _client;
        private bool _connected;

        void Awake()
        {
            Connect();
        }
        public void Update()
        {
            if (_connected)
            {
                Send();
            }
        }

        public void Connect()
        {
            if (_connected)
                return;
            try
            {
                _client = new UdpClient();
                _client.Connect(IPAddress.Broadcast, 3910);
            } catch (Exception e)
            {
                Debug.LogError(e);
            }
            _connected = true;
        }
        public void Disconnect()
        {
            if (!_connected)
                return;
            _client?.Dispose();
            _connected = false;
        }

        public void Send()
        {
            var pos = new TrackerPositions(trackers);
            var bytes = MessagePackSerializer.Serialize(pos);
            _client.SendAsync(bytes, bytes.Length);
        }

        public void OnApplicationQuit()
        {
            _client.Close();
        }
    }
    
    
    
    

}