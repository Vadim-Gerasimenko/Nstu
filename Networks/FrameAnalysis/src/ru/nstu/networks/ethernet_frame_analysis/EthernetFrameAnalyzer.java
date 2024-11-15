package ru.nstu.networks.ethernet_frame_analysis;

import java.io.*;
import java.nio.ByteBuffer;

public class EthernetFrameAnalyzer {
    public static final String BINARY_EXTENSION = ".bin";

    public static final String ETHERNET_2_DIX = "Ethernet II (DIX)";
    public static final String LLC = "Ethernet 802.2 (LLC)";
    public static final String SNAP = "Ethernet 802.2 (SNAP)";
    public static final String NOVELL = "Ethernet 802.3 (Raw/Novell)";
    public static final int SNAP_CODE = 0x00AA;
    public static final int NOVELL_CODE = 0xFFFF;

    public static final int ETHERNET_802_2_CODE_SIZE = 0x0002;

    public static final String ICMP = "ICMP";
    public static final String TCP = "TCP";
    public static final String UDP = "UDP";
    public static final int ICMP_NUMBER = 0x0001;
    public static final int TCP_NUMBER = 0x0006;
    public static final int UDP_NUMBER = 0x0011;

    public static final String IP_VERSION_4 = "IPv4";
    public static final String ARP = "ARP";
    public static final String RARP = "RARP";
    public static final int IP_VERSION_4_NUMBER = 0x0800;
    public static final int ARP_NUMBER = 0x0806;
    public static final int RARP_NUMBER = 0x0835;

    public static final String REQUEST = "Request";
    public static final String REPLY = "Reply";
    public static final int REQUEST_NUMBER = 0x0001;
    public static final int REPLY_NUMBER = 0x0002;

    public static final int MAX_FRAME_SIZE = 0xFFFF;
    public static final int MAC_ADDRESS_SIZE = 0x0006;
    public static final int TYPE_OR_LENGTH_SIZE = 0x0002;
    public static final int VERSION_AND_HEADER_LENGTH_SIZE = 0x0001;

    public static final int VERSION_BITS_COUNT = 0x0004;
    public static final int WORD_32_BITS_SIZE_IN_BYTES = 0x0004;

    public static final int TOS_SIZE = 0x0001;
    public static final int TTL_SIZE = 0x0001;
    public static final int IP_SIZE = 0x0004;

    public static final String ETHERNET_HARDWARE_TYPE = "Ethernet";
    public static final int ETHERNET_HARDWARE_TYPE_NUMBER = 0x0001;
    public static final int HARDWARE_ADDRESS_LENGTH_SIZE = 0x0001;
    public static final int HARDWARE_TYPE_SIZE = 0x0002;
    public static final int PROTOCOL_ADDRESS_LENGTH_SIZE = 0x0001;
    public static final int PROTOCOL_TYPE_SIZE = 0x0002;

    public static final int DATAGRAM_LENGTH_SIZE = 0x0002;
    public static final int DATAGRAM_FIELD_MAX_SIZE = 0x05DC;

    public static final int IDENTIFIER_SIZE = 0x0002;
    public static final int OPTIONS_AND_FRAGMENT_OFFSET_SIZE = 0x0002;
    public static final int UPPER_LEVEL_PROTOCOL_SIZE = 0x0001;
    public static final int HEADER_CHECKSUM_SIZE = 0x0002;
    public static final int OPERATION_SIZE = 0x0002;

    private EthernetFrameAnalyzer() {
    }

    public static void analyze(File binaryFile, PrintStream out) {
        validateBinaryFile(binaryFile);

        try (BufferedInputStream stream = new BufferedInputStream(new FileInputStream(binaryFile))) {
            int fileSize = (int) binaryFile.length();
            out.println("File name: " + binaryFile.getName());
            out.printf("File size: %d%n", fileSize);

            int framesCount = 0;
            int dixFramesCount = 0;
            int novellFramesCount = 0;
            int snapFramesCount = 0;
            int llcFramesCount = 0;
            int ip4FramesCount = 0;
            int arpFramesCount = 0;
            int rarpFramesCount = 0;

            for (int readBytesCount = 0; readBytesCount < fileSize; ) {
                int startIndex = readBytesCount;
                int index = startIndex;

                ++framesCount;
                out.printf("%nFRAME #%d%n", framesCount);
                out.println(readBytesCount);
                byte[] bytes = new byte[MAX_FRAME_SIZE];

                readBytesCount += read(stream, bytes, index, MAC_ADDRESS_SIZE);
                out.println("MAC (receiver): " + getMacAddress(bytes, index));
                index += MAC_ADDRESS_SIZE;

                readBytesCount += read(stream, bytes, index, MAC_ADDRESS_SIZE);
                out.println("MAC (sender): " + getMacAddress(bytes, index));
                index += MAC_ADDRESS_SIZE;

                readBytesCount += read(stream, bytes, index, TYPE_OR_LENGTH_SIZE);
                int dataFieldSize = convert2BytesToInt(bytes, index);
                index += TYPE_OR_LENGTH_SIZE;

                if (dataFieldSize > DATAGRAM_FIELD_MAX_SIZE) {
                    out.println("Frame type: " + ETHERNET_2_DIX);
                    ++dixFramesCount;

                    switch (dataFieldSize) {
                        case IP_VERSION_4_NUMBER:
                            int headerStartIndex = index;
                            readBytesCount += read(stream, bytes, index, VERSION_AND_HEADER_LENGTH_SIZE);
                            String versionAndHeaderLength = convertByteToBin(bytes[index]);
                            index += VERSION_AND_HEADER_LENGTH_SIZE;

                            out.println("Protocol type: " + IP_VERSION_4);
                            ++ip4FramesCount;

                            int headerLength = Integer.parseInt(versionAndHeaderLength.substring(VERSION_BITS_COUNT), 2)
                                    * WORD_32_BITS_SIZE_IN_BYTES;
                            out.println("Header length: " + headerLength + " bytes");

                            readBytesCount += read(stream, bytes, index, TOS_SIZE);
                            out.println("Type of service: " + convertByteToInt(bytes[index]));
                            index += TOS_SIZE;

                            readBytesCount += read(stream, bytes, index, DATAGRAM_LENGTH_SIZE);
                            int dataLength = convert2BytesToInt(bytes, index);
                            out.println("Data length: " + dataLength + " bytes");
                            index += DATAGRAM_LENGTH_SIZE;

                            readBytesCount += read(stream, bytes, index, IDENTIFIER_SIZE + OPTIONS_AND_FRAGMENT_OFFSET_SIZE);
                            //identifier, options and fragment offset skipped
                            index += IDENTIFIER_SIZE + OPTIONS_AND_FRAGMENT_OFFSET_SIZE;

                            readBytesCount += read(stream, bytes, index, TTL_SIZE);
                            out.println("TTL: " + convertByteToInt(bytes[index]));
                            index += TTL_SIZE;

                            readBytesCount += read(stream, bytes, index, UPPER_LEVEL_PROTOCOL_SIZE);
                            out.println("Protocol: " + getProtocolName(bytes, index));
                            index += UPPER_LEVEL_PROTOCOL_SIZE;

                            readBytesCount += read(stream, bytes, index, HEADER_CHECKSUM_SIZE);
                            out.println("Header checksum: " + convert2BytesToInt(bytes, index));
                            index += HEADER_CHECKSUM_SIZE;

                            readBytesCount += read(stream, bytes, index, IP_SIZE);
                            out.println("IP (sender): " + getIpAddress(bytes, index));
                            index += IP_SIZE;

                            readBytesCount += read(stream, bytes, index, IP_SIZE);
                            out.println("IP (receiver): " + getIpAddress(bytes, index));
                            index += IP_SIZE;

                            int unreadHeaderSize = index - startIndex
                                    - headerLength - 2 * MAC_ADDRESS_SIZE - TYPE_OR_LENGTH_SIZE;

                            if (unreadHeaderSize != headerStartIndex) {
                                //options skipped
                                readBytesCount += read(stream, bytes, index, unreadHeaderSize);
                                index += unreadHeaderSize;
                            }

                            readBytesCount += read(stream, bytes, index, dataLength - headerLength);
                            out.println("Data: " + concatenateData(bytes, index, readBytesCount));
                            break;
                        case ARP_NUMBER, RARP_NUMBER:
                            if (dataFieldSize == ARP_NUMBER) {
                                out.println("Protocol type: " + ARP);
                                ++arpFramesCount;
                            } else {
                                out.println("Protocol type: " + RARP);
                                ++rarpFramesCount;
                            }

                            readBytesCount += read(stream, bytes, index, HARDWARE_TYPE_SIZE);
                            int hardwareTypeNumber = convert2BytesToInt(bytes, index);
                            index += HARDWARE_TYPE_SIZE;

                            String hardwareType = hardwareTypeNumber == ETHERNET_HARDWARE_TYPE_NUMBER
                                    ? ETHERNET_HARDWARE_TYPE
                                    : hardwareTypeNumber + " (missing from the database)";
                            out.println("Hardware type: " + hardwareType);

                            readBytesCount += read(stream, bytes, index, PROTOCOL_TYPE_SIZE);
                            int protocolTypeNumber = convert2BytesToInt(bytes, index);
                            String protocolType = protocolTypeNumber == IP_VERSION_4_NUMBER
                                    ? IP_VERSION_4
                                    : protocolTypeNumber + " (missing from the database)";
                            out.println("Protocol type: " + protocolType);
                            index += PROTOCOL_TYPE_SIZE;

                            readBytesCount += read(stream, bytes, index, HARDWARE_ADDRESS_LENGTH_SIZE);
                            int hardwareAddressLength = convertByteToInt(bytes[index]);
                            out.println("Hardware address length: " + hardwareAddressLength + " bytes");
                            index += HARDWARE_ADDRESS_LENGTH_SIZE;

                            readBytesCount += read(stream, bytes, index, PROTOCOL_ADDRESS_LENGTH_SIZE);
                            int protocolAddressLength = convertByteToInt(bytes[index]);
                            out.println("Protocol address length: " + protocolAddressLength + " bytes");
                            index += PROTOCOL_ADDRESS_LENGTH_SIZE;

                            readBytesCount += read(stream, bytes, index, OPERATION_SIZE);
                            int operationNumber = convert2BytesToInt(bytes, index);
                            String operation = switch (operationNumber) {
                                case REQUEST_NUMBER -> REQUEST;
                                case REPLY_NUMBER -> REPLY;
                                default -> operationNumber + " (missing from the database)";
                            };
                            out.println("Operation type: " + operation);
                            index += OPERATION_SIZE;

                            readBytesCount += read(stream, bytes, index, hardwareAddressLength);
                            out.println("Hardware address (sender): " + getMacAddress(bytes, index));
                            index += hardwareAddressLength;

                            readBytesCount += read(stream, bytes, index, protocolAddressLength);
                            out.println("Protocol address (sender): " + getIpAddress(bytes, index));
                            index += protocolAddressLength;

                            readBytesCount += read(stream, bytes, index, hardwareAddressLength);
                            out.println("Hardware address (receiver): " + getMacAddress(bytes, index));
                            index += hardwareAddressLength;

                            readBytesCount += read(stream, bytes, index, protocolAddressLength);
                            out.println("Protocol address (receiver): " + getIpAddress(bytes, index));
                            break;
                        default:
                            out.println("Protocol type number " + dataFieldSize + " (missing from the database)");
                    }
                } else {
                    readBytesCount += read(stream, bytes, index, ETHERNET_802_2_CODE_SIZE);
                    int dataFirst2Bytes = convert2BytesToInt(bytes, index);

                    switch (dataFirst2Bytes) {
                        case NOVELL_CODE:
                            ++novellFramesCount;
                            out.println("Protocol type: " + NOVELL);
                            //dataFieldSize = Math.max(dataFieldSize, DATAGRAM_FIELD_MIN_SIZE);
                            break;
                        case SNAP_CODE:
                            ++snapFramesCount;
                            out.println("Protocol type: " + SNAP);
                            //dataFieldSize = Math.max(dataFieldSize, DATAGRAM_FIELD_MIN_SIZE) + LLC_HEADER_SIZE + SNAP_HEADER_SIZE;
                            break;
                        default:
                            ++llcFramesCount;
                            out.println("Protocol type: " + LLC);
                            //dataFieldSize = Math.max(dataFieldSize, DATAGRAM_FIELD_MIN_SIZE) + LLC_HEADER_SIZE;
                    }

                    out.println("Data length: " + dataFieldSize + " bytes");

                    readBytesCount += read(stream, bytes, index, dataFieldSize - ETHERNET_802_2_CODE_SIZE);
                    out.println("Data: " + concatenateData(bytes, index - ETHERNET_802_2_CODE_SIZE,
                            index + dataFieldSize - ETHERNET_802_2_CODE_SIZE));
                }
            }

            out.printf("%n-------------------------------------");
            out.printf("%nFrames count: %d%n", framesCount);
            out.printf("%n[Types]%n");
            out.println(LLC + ": " + llcFramesCount);
            out.println(SNAP + ": " + snapFramesCount);
            out.println(NOVELL + ": " + novellFramesCount);
            out.println(ETHERNET_2_DIX + ": " + dixFramesCount);
            out.println("\t" + IP_VERSION_4 + ": " + ip4FramesCount);
            out.println("\t" + ARP + ": " + arpFramesCount);
            out.println("\t" + RARP + ": " + rarpFramesCount);
            out.println("-------------------------------------");
        } catch (IOException e) {
            out.println("Error reading file \"" + binaryFile.getName() + "\": " + e.getMessage());
        }
    }

    private static int read(InputStream stream, byte[] array, int startIndex, int length) throws IOException {
        int readBytesCount;
        int offset = startIndex;
        int end = length + startIndex;

        while (offset != end) {
            readBytesCount = stream.read(array, offset, length);
            offset += readBytesCount;
        }

        return offset - startIndex;
    }

    private static String convertByteToHex(byte number) {
        char[] hexDigits = new char[2];
        hexDigits[0] = Character.forDigit((number >> 4) & 0xF, 16);
        hexDigits[1] = Character.forDigit((number & 0xF), 16);
        return new String(hexDigits).toUpperCase();
    }

    private static String convertByteToBin(byte number) {
        return String.format("%8s", Integer.toBinaryString(number & 0xFF)).replace(' ', '0');
    }

    private static int convertByteToInt(byte number) {
        return number & 0xFF;
    }

    private static int convert2BytesToInt(byte[] array, int startIndex) {
        final int intLength = 4;
        final int dataLength = 2;
        byte[] bytes = new byte[intLength];

        System.arraycopy(array, startIndex, bytes, intLength - dataLength, dataLength);
        return ByteBuffer.wrap(bytes).getInt();
    }

    private static String concatenateData(byte[] array, int startIndex, int endIndex) {
        StringBuilder sb = new StringBuilder();
        int index = startIndex;

        while (index < endIndex) {
            sb.append(convertByteToHex(array[index]));
            ++index;
        }

        return sb.toString();
    }

    private static String getIpAddress(byte[] array, int startIndex) {
        StringBuilder sb = new StringBuilder();
        final char separator = '.';

        for (int i = 0; i < IP_SIZE; ++i) {
            int number = array[startIndex + i];
            sb.append(number & 0xFF).append(separator);
        }

        return sb.deleteCharAt(sb.length() - 1).toString();
    }

    private static String getMacAddress(byte[] array, int startIndex) {
        StringBuilder sb = new StringBuilder();
        final char separator = '-';

        for (int i = 0; i < MAC_ADDRESS_SIZE; ++i) {
            sb.append(convertByteToHex(array[i + startIndex])).append(separator);
        }

        return sb.deleteCharAt(sb.length() - 1).toString();
    }

    private static String getProtocolName(byte[] bytes, int startIndex) {
        return switch (bytes[startIndex]) {
            case ICMP_NUMBER -> ICMP;
            case TCP_NUMBER -> TCP;
            case UDP_NUMBER -> UDP;
            default -> bytes[startIndex] + " (missing from the database)";
        };
    }

    private static void validateBinaryFile(File binaryFile) {
        if (binaryFile == null) {
            throw new NullPointerException("File references to null");
        }

        String fileName = binaryFile.getName();

        if (!fileName.endsWith(BINARY_EXTENSION)) {
            throw new IllegalArgumentException("File must have a binary extension. "
                    + "Current extension: " + fileName.substring(fileName.lastIndexOf(".")));
        }
    }
}