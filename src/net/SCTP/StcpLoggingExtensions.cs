using System;
using Microsoft.Extensions.Logging;
using SIPSorcery.Net;

namespace SIPSorcery.net.SCTP
{
    internal static partial class StcpLoggingExtensions
    {
        [LoggerMessage(
            EventId = 0,
            EventName = "SctpPacketReceivedAborted",
            Level = LogLevel.Warning,
            Message = "SCTP packet received but association has been aborted, ignoring."
            )]
        public static partial void LogSctpPacketReceivedAborted(this ILogger logger);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpPacketDroppedWrongVerificationTag",
            Level = LogLevel.Warning,
            Message = "SCTP packet dropped due to wrong verification tag, expected {ExpectedVerificationTag} got {ReceivedVerificationTag}."
            )]
        public static partial void LogSctpPacketDroppedWrongVerificationTag(this ILogger logger, uint expectedVerificationTag, uint receivedVerificationTag);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpPacketDroppedWrongDestinationPort",
            Level = LogLevel.Warning,
            Message = "SCTP packet dropped due to wrong SCTP destination port, expected {ExpectedDestinationPort} got {ReceivedDestinationPort}."
            )]
        public static partial void LogSctpPacketDroppedWrongDestinationPort(this ILogger logger, ushort expectedDestinationPort, ushort receivedDestinationPort);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpPacketDroppedWrongSourcePort",
            Level = LogLevel.Warning,
            Message = "SCTP packet dropped due to wrong SCTP source port, expected {ExpectedSourcePort} got {ReceivedSourcePort}."
            )]
        public static partial void LogSctpPacketDroppedWrongSourcePort(this ILogger logger, ushort expectedSourcePort, ushort receivedSourcePort);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpDataChunkReceived",
            Level = LogLevel.Trace,
            Message = "SCTP data chunk received on ID {ID} with TSN {TSN}, payload length {PayloadLength}, flags {Flags}."
            )]
        public static partial void LogSctpDataChunkReceived(this ILogger logger, string id, uint tsn, int payloadLength, byte flags);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpPacketAbortChunkReceived",
            Level = LogLevel.Warning,
            Message = "SCTP packet ABORT chunk received from remote party, reason {Reason}."
            )]
        public static partial void LogSctpPacketAbortChunkReceived(this ILogger logger, string reason);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpErrorReceived",
            Level = LogLevel.Warning,
            Message = "SCTP error {CauseCode}."
            )]
        public static partial void LogSctpErrorReceived(this ILogger logger, SctpErrorCauseCode causeCode);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpSendingShutdown",
            Level = LogLevel.Trace,
            Message = "SCTP sending shutdown for association {ID}, ACK TSN {ackTSN}."
            )]
        public static partial void LogSctpSendingShutdown(this ILogger logger, string id, uint? ackTSN);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpStateChanged",
            Level = LogLevel.Trace,
            Message = "SCTP state for association {ID} changed to {State}."
            )]
        public static partial void LogSctpStateChanged(this ILogger logger, string id, SctpAssociationState state);

        [LoggerMessage(
            EventId = 0,
            EventName = "ReceivedChunk",
            Level = LogLevel.Trace,
            Message = "SCTP receiver got data chunk with TSN {TSN}, last in order TSN {LastInOrderTSN}, in order receive count {InOrderReceiveCount}."
            )]
        public static partial void LogReceivedChunk(this ILogger logger, uint tsn, uint lastInOrderTSN, uint inOrderReceiveCount);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpDuplicateTsnReceived",
            Level = LogLevel.Trace,
            Message = "SCTP duplicate TSN received for {TSN}."
            )]
        public static partial void LogSctpDuplicateTsnReceived(this ILogger logger, uint tsn);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpSenderExitingRetransmitMode",
            Level = LogLevel.Trace,
            Message = "SCTP sender exiting retransmit mode."
            )]
        public static partial void LogSctpSenderExitingRetransmitMode(this ILogger logger);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpFirstSackReceived",
            Level = LogLevel.Trace,
            Message = "SCTP first SACK remote peer TSN ACK {CumulativeTsnAck} next sender TSN {TSN}, arwnd {ARwnd} (gap reports {GapAckBlocksCount})."
            )]
        public static partial void LogSctpFirstSackReceived(this ILogger logger, uint cumulativeTsnAck, uint tsn, uint arwnd, int gapAckBlocksCount);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpSackTsnTooDistant",
            Level = LogLevel.Warning,
            Message = "SCTP SACK TSN from remote peer of {CumulativeTsnAck} was too distant from the expected {CumulativeAckTSN}, ignoring."
            )]
        public static partial void LogSctpSackTsnTooDistant(this ILogger logger, uint cumulativeTsnAck, uint cumulativeAckTSN);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpSackTsnBehindExpected",
            Level = LogLevel.Warning,
            Message = "SCTP SACK TSN from remote peer of {CumulativeTsnAck} was behind expected {CumulativeAckTSN}, ignoring."
            )]
        public static partial void LogSctpSackTsnBehindExpected(this ILogger logger, uint cumulativeTsnAck, uint cumulativeAckTSN);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpSackReceived",
            Level = LogLevel.Trace,
            Message = "SCTP SACK remote peer TSN ACK {CumulativeTsnAck}, next sender TSN {TSN}, arwnd {ARwnd} (gap reports {GapAckBlocksCount})."
            )]
        public static partial void LogSctpSackReceived(this ILogger logger, uint cumulativeTsnAck, uint tsn, uint arwnd, int gapAckBlocksCount);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpSackReceivedNoChange",
            Level = LogLevel.Trace,
            Message = "SCTP SACK remote peer TSN ACK no change {CumulativeAckTSN}, next sender TSN {TSN}, arwnd {ARwnd} (gap reports {GapAckBlocksCount})."
            )]
        public static partial void LogSctpSackReceivedNoChange(this ILogger logger, uint cumulativeAckTSN, uint tsn, uint arwnd, int gapAckBlocksCount);

        [LoggerMessage(
            EventId = 0,
            EventName = "ExitingFastRecovery",
            Level = LogLevel.Trace,
            Message = "SCTP sender exiting fast recovery at TSN {FastRecoveryExitPoint}"
            )]
        public static partial void LogExitingFastRecovery(this ILogger logger, uint fastRecoveryExitPoint);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpAcknowledgedDataChunkReceipt",
            Level = LogLevel.Trace,
            Message = "SCTP acknowledged data chunk receipt in gap report for TSN {TSN}"
            )]
        public static partial void LogSctpAcknowledgedDataChunkReceipt(this ILogger logger, uint tsn);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpSackGapReport",
            Level = LogLevel.Trace,
            Message = "SCTP SACK gap report start TSN {goodTSNStart} gap report end TSN {gapBlockEnd} first missing TSN {missingTSN}."
            )]
        public static partial void LogSctpSackGapReport(this ILogger logger, uint goodTSNStart, uint gapBlockEnd, uint missingTSN);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpSackGapAddingRetransmitEntry",
            Level = LogLevel.Trace,
            Message = "SCTP SACK gap adding retransmit entry for TSN {TSN}."
            )]
        public static partial void LogSctpSackGapAddingRetransmitEntry(this ILogger logger, uint tsn);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpSenderEnteringFastRecoveryMode",
            Level = LogLevel.Trace,
            Message = "SCTP sender entering fast recovery mode due to missing TSN {MissingTsn}. Fast recovery exit point {FastRecoveryExitPoint}."
            )]
        public static partial void LogSctpSenderEnteringFastRecoveryMode(this ILogger logger, uint missingTsn, uint fastRecoveryExitPoint);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpSenderRemovingUnconfirmedChunks",
            Level = LogLevel.Trace,
            Message = "SCTP data sender removing unconfirmed chunks cumulative ACK TSN {CumulativeAckTsn}, SACK TSN {SackTsn}."
            )]
        public static partial void LogSctpSenderRemovingUnconfirmedChunks(this ILogger logger, uint cumulativeAckTSN, uint sackTSN);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpResendingMissingDataChunk",
            Level = LogLevel.Trace,
            Message = "SCTP resending missing data chunk for TSN {TSN}, data length {UserDataLength}, flags {ChunkFlags:X2}, send count {SendCount}."
            )]
        public static partial void LogSctpResendingMissingDataChunk(this ILogger logger, uint tSN, int userDataLength, byte chunkFlags, int sendCount);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpRetransmittingDataChunk",
            Level = LogLevel.Trace,
            Message = "SCTP retransmitting data chunk for TSN {Tsn}, data length {DataLength}, flags {ChunkFlags}, send count {SendCount}."
            )]
        public static partial void LogSctpRetransmittingDataChunk(this ILogger logger, uint tSN, int dataLength, byte chunkFlags, int sendCount);

        [LoggerMessage(
            EventId = 0,
            EventName = "EnteringRetransmitMode",
            Level = LogLevel.Trace,
            Message = "SCTP sender entering retransmit mode."
            )]
        public static partial void LogEnteringRetransmitMode(this ILogger logger);

        [LoggerMessage(
            EventId = 0,
            EventName = "SctpResendingMissingDataChunk2",
            Level = LogLevel.Trace,
            Message = "SCTP resending missing data chunk for TSN {TSN}, data length {UserDataLength}, flags {ChunkFlags:X2}, send count {SendCount}."
            )]
        public static partial void LogSctpResendingMissingDataChunk2(this ILogger logger, uint tSN, int userDataLength, byte chunkFlags, int sendCount);

        [LoggerMessage(
            EventId = 0,
            EventName = "SlowStartIncreased",
            Level = LogLevel.Trace,
            Message = "SCTP sender congestion window in slow-start increased from {OldCongestionWindow} to {NewCongestionWindow}."
            )]
        public static partial void LogSlowStartIncreased(this ILogger logger, uint oldCongestionWindow, uint newCongestionWindow);

        [LoggerMessage(
            EventId = 0,
            EventName = "CongestionAvoidanceIncreased",
            Level = LogLevel.Trace,
            Message = "SCTP sender congestion window in congestion avoidance increased from {OldCongestionWindow} to {NewCongestionWindow}."
            )]
        public static partial void LogCongestionAvoidanceIncreased(this ILogger logger, uint oldCongestionWindow, uint newCongestionWindow);
    }
}
