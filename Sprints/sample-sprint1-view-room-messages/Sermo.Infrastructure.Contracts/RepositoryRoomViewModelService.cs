﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sermo.Data.Contracts;
using Sermo.Infrastructure.Contracts;
using Sermo.UI.Contracts;

namespace Sermo.UI.Controllers
{
    public class RepositoryRoomViewModelService : IRoomViewModelReader, IRoomViewModelWriter
    {
        public RepositoryRoomViewModelService(IRoomRepository roomRepository, IMessageRepository messageRepository, IRoomViewModelMapper mapper)
        {
            Contract.Requires<ArgumentNullException>(roomRepository != null);
            Contract.Requires<ArgumentNullException>(mapper != null);

            this.roomRepository = roomRepository;
            this.messageRepository = messageRepository;
            this.mapper = mapper;
        }

        public IEnumerable<RoomViewModel> GetAllRooms()
        {
            var allRooms = new List<RoomViewModel>();
            var allRoomRecords = roomRepository.GetAllRooms();
            foreach(var roomRecord in allRoomRecords)
            {
                allRooms.Add(mapper.MapRoomRecordToRoomViewModel(roomRecord));
            }
            return allRooms;
        }

        public IEnumerable<MessageViewModel> GetRoomMessages(int roomID)
        {
            var roomMessages = new List<MessageViewModel>();
            var roomMessageRecords = messageRepository.GetMessagesForRoomID(roomID);
            foreach(var messageRecord in roomMessageRecords)
            {
                roomMessages.Add(mapper.MapMessageRecordToMessageViewModel(messageRecord));
            }
            return roomMessages;
        }

        public void CreateRoom(string roomName)
        {
            roomRepository.CreateRoom(roomName);
        }

        private readonly IRoomRepository roomRepository;
        private readonly IMessageRepository messageRepository;
        private readonly IRoomViewModelMapper mapper;
    }
}