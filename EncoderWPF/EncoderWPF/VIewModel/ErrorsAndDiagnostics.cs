using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderWPF
{
    internal static class ErrorsAndDiagnostics
    {
        public static void DeterminationOfMastTypeAnswer(List<int> requestValidationResult, MainViewModel _mainViewModel)
        {
            if (requestValidationResult.Count > 0)
            {
                if (requestValidationResult[0] == 1)
                {
                    _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Установлен тип мачты: ПМ-1\n";
                    _mainViewModel.MystTypeComboBoxText = "ПМ-1";
                    _mainViewModel.MastPositionProgressBarMinimum = "1580";
                }
                else if (requestValidationResult[0] == 2)
                {
                    _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Установлен тип мачты: ПМ-2\n";
                    _mainViewModel.MystTypeComboBoxText = "ПМ-2";
                    _mainViewModel.MastPositionProgressBarMinimum = "1980";
                }
                else if (requestValidationResult[0] == 3)
                {
                    _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Установлен тип мачты: ПМ-3\n";
                    _mainViewModel.MystTypeComboBoxText = "ПМ-3";
                    _mainViewModel.MastPositionProgressBarMinimum = "1776";
                }
            }
            else
            {
                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка запроса данных после изменения типа мачты\n";
                //listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
            }
        }
        public static string ChangeZeroPositionValueAnswer(List<int> requestValidationResult)
        {
            if (requestValidationResult.Count > 0)
            {
                if (requestValidationResult[0] == 201)
                {
                    return $"{DateTime.Now} Успешная установка нулевого положения\n";
                }
                else if (requestValidationResult[0] == 243)
                {
                    return $"{DateTime.Now} Ошибка установки нулевого положения мачты\n";
                }
                else
                {
                    return $"{DateTime.Now} Неизвестная ошибка\n";
                }
            }
            else
            {
                return $"{DateTime.Now} Ошибка запроса данных после установки нулевого положения\n";
            }
        }
        public static string ChangeSpeedAnswer(List<int> requestValidationResult, MainViewModel _mainViewModel, string newSpeed)
        {
            if (requestValidationResult.Count > 0)
            {
                if (requestValidationResult[0] == 1)
                {
                    _mainViewModel.SpeedComboBoxPropertyText = newSpeed.ToString();
                    return $"{DateTime.Now} Скорость успешно изменена\n";
                }
                else if (requestValidationResult[0] == 4)
                {
                    return $"{DateTime.Now} Некорректная скорость\n";
                }
                else if (requestValidationResult[0] == 5)
                {
                    return $"{DateTime.Now} Ошибка сохранения в EEPROM\n";
                }
                else
                {
                    return "Неизвестная ошибка\n";
                }
            }
            else
            {
                return $"{DateTime.Now} Ошибка запроса данных после изменения скорости\n";
            }
        }
        public static string ChangeAddressAnswer(List<int> requestValidationResult, MainViewModel _mainViewModel, byte newAddr)
        {
            if (requestValidationResult.Count > 0)
            {
                if (requestValidationResult[0] == 0)
                {
                    _mainViewModel.AddressDeviceNumericUpDownValue = newAddr;
                    return $"{DateTime.Now} Адрес устройства успешно изменен\n";
                }
                else if (requestValidationResult[0] == 250)
                {
                    return $"{DateTime.Now} Адрес не установлен\n";
                }
                else if (requestValidationResult[0] == 251)
                {
                    return $"{DateTime.Now} Некорректный адрес\n";
                }
                else if (requestValidationResult[0] == 252)
                {
                    return $"{DateTime.Now} Ошибка сохранения в EEPROM\n";
                }
                else
                {
                    return "Неизвестная ошибка\n";
                }
            }
            else
            {
                return $"{DateTime.Now} Ошибка запроса данных после изменения адреса устройства\n";
            }
        }
        public static void GetTypeError(List<int> requestMassDataMast, MainViewModel _mainViewModel)
        {
            if (requestMassDataMast.Count > 0)
            {
                int typeError = requestMassDataMast[0];
                byte lowByte = (byte)(typeError); // младший байт 
                byte highByte = (byte)(typeError >> 8); // старший байт 
                byte mask = 0x01; // начальная битовая маска

                if (typeError == 0)
                    return;

                for (int i = 0; i < 8; i++)
                {
                    byte errorlowByte = (byte)(lowByte & mask);
                    byte errorhighByte = (byte)(highByte & mask);

                    switch (errorlowByte)
                    {
                        case 0b0000:
                            {
                                break;
                            }
                        case 0b0001:
                            {
                                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка статус энкодера\n";
                                break;
                            }
                        case 0b0010:
                            {
                                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка положения мачты\n";
                                break;
                            }
                        case 0b0100:
                            {
                                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка источника питания\n";
                                break;
                            }
                        case 0b1000:
                            {
                                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка напряжения 3.3В\n";
                                break;
                            }
                        case 0b00010000:
                            {
                                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка напряжения 5.36В\n";
                                break;
                            }
                        case 0b00100000:
                            {
                                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка напряжения 27В\n";
                                break;
                            }
                        case 0b01000000:
                            {
                                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка температуры\n";
                                break;
                            }
                        case 0b10000000:
                            {
                                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка скорости RS-485\n";
                                break;
                            }
                    }

                    switch (errorhighByte)
                    {
                        case 0b0000:
                            {
                                break;
                            }
                        case 0b0001:
                            {
                                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка адреса на шине RS-485\n";
                                break;
                            }
                        case 0b0010:
                            {
                                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка EEPROM памяти\n";
                                break;
                            }
                        case 0b0100:
                            {
                                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка статуса запуска\n";
                                break;
                            }
                        case 0b1000:
                            {
                                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка CRC flash памяти\n";
                                break;
                            }
                        case 0b00010000:
                            {
                                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка, не выбран тип мачты\n";
                                break;
                            }
                    }
                    mask = (byte)(mask << 1); // сдвигаем маску на один шаг вправо
                    //Thread.Sleep( 1000 );
                }
            }
            else
            {
                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка запроса данных об ошибках\n";
            }
        }
        public static void DiagnostEncoder(List<int> resultDiagnost, MainViewModel _mainViewModel)
        {
            if (resultDiagnost.Count > 0)
            {
                int typeError = resultDiagnost[0];
                byte lowByte = (byte)(typeError); // младший байт 
                byte highByte = (byte)(typeError >> 8); // старший байт 
                byte mask = 0x01; // начальная битовая маска

                byte Check_Parity = (byte)(lowByte & mask);

                if (Check_Parity == 0)
                {
                    _mainViewModel.ErrorMessageListBoxText += $"Бит четности не совпадает с рассчитанным\n";
                }
                else
                {
                    _mainViewModel.ErrorMessageListBoxText += $"Бит четности совпадает с рассчитанным\n";
                }

                mask = (byte)(mask << 1);
                byte MagDEC = (byte)(lowByte & mask);

                mask = (byte)(mask << 1);
                byte MagINC = (byte)(lowByte & mask);

                mask = (byte)(mask << 1);
                byte LIN = (byte)(lowByte & mask);

                if (MagDEC == 0 && MagINC == 0)
                {
                    _mainViewModel.ErrorMessageListBoxText += $"Дистанция не изменяется, GREEN уровень намагниченности\n";
                }
                else if (MagDEC == 0b0001 && MagINC == 0)
                {
                    _mainViewModel.ErrorMessageListBoxText += $"Дистанция до магнита увеличивается\n";
                }
                else if (MagDEC == 0 && MagINC == 0b0001)
                {
                    _mainViewModel.ErrorMessageListBoxText += $"Дистанция до магнита уменьшается\n";
                }
                else if (MagDEC == 0b0001 && MagINC == 0b0001 && LIN == 0)
                {
                    _mainViewModel.ErrorMessageListBoxText += $"YELLOW уровень намагниченности\n";
                }
                else if (MagDEC == 0b0001 && MagINC == 0b0001 && LIN == 0b0001)
                {
                    _mainViewModel.ErrorMessageListBoxText += $"RED уровень намагниченности\n";
                }

                mask = (byte)(mask << 1);
                byte COF = (byte)(lowByte & mask);

                if (COF == 0)
                {
                    _mainViewModel.ErrorMessageListBoxText += $"Данные углового положения находятся в рамках диапазона\n";
                }
                else
                {
                    _mainViewModel.ErrorMessageListBoxText += $"Данные углового положения находятся вне диапазона, необходима калибровка положения магнита\n";
                }

                mask = (byte)(mask << 1);
                byte OCF = (byte)(lowByte & mask);

                if (OCF == 0)
                {
                    _mainViewModel.ErrorMessageListBoxText += $"Компенсация не завершена\n";
                }
                else
                {
                    _mainViewModel.ErrorMessageListBoxText += $"Компенсация завершена\n";
                }

            }
            else
            {
                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка запроса данных об ошибках\n";
            }
        }
    }
}
