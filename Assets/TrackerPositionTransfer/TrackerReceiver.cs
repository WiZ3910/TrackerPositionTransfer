using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MessagePack;
using UnityEngine;
using UnityEngine.UI;

namespace TrackerPositionTransfer
{

    public class TrackerReceiver: MonoBehaviour
    {

        public Transform[] trackers;

        private UdpClient _client;

        //[SerializeField] private string ip;
      //  [SerializeField] private int port;

        private IPEndPoint _ipEndPoint;
        private TrackerPositions current;

        private bool _connected;

        private void Start()
        {
            ReceiveEvent += b =>
            {
                try
                {
                    current = MessagePackSerializer.Deserialize<TrackerPositions>(b);
                    Apply();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            };

            Connect();

            Receive();

        }
        public void Connect()
        {
            if (_connected) 
                return;

            try
            {
                _ipEndPoint = new IPEndPoint(IPAddress.Any, 3910);

                _client = new UdpClient(_ipEndPoint);
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

        private void Apply()
        {
            if (current != null)
            {
                int end = new[]
                    {current.trackerPositions.Length, current.trackerRotations.Length, trackers.Length}.Max();
                for (int i = 0; i < end; i++)
                {
                    trackers[i].position = current.trackerPositions[i];
                    trackers[i].rotation = current.trackerRotations[i];
                }
            }
        }
        
        async Task Receive()
        {
            var s = await _client.ReceiveAsync();
            ReceiveEvent?.Invoke(s.Buffer);
            Receive();
        }

        public void OnApplicationQuit()
        {
            _client.Close();
        }

        public event Action<byte[]> ReceiveEvent;

    }
}