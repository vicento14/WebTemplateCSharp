-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 06, 2023 at 03:14 PM
-- Server version: 10.4.14-MariaDB
-- PHP Version: 7.4.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `web_template_app`
--

-- --------------------------------------------------------

--
-- Table structure for table `migrations`
--

CREATE TABLE `migrations` (
  `id` int(10) UNSIGNED NOT NULL,
  `migration` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `batch` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `migrations`
--

INSERT INTO `migrations` (`id`, `migration`, `batch`) VALUES
(1, '2023_11_21_002309_create_user_accounts_table', 1),
(2, '2023_11_26_203810_create_t_exercises_table', 2),
(3, '2023_11_26_204514_create_t_t1_s_table', 2),
(4, '2023_11_26_204541_create_t_t2_s_table', 2);

-- --------------------------------------------------------

--
-- Table structure for table `t_exercises`
--

CREATE TABLE `t_exercises` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `details` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `t_t1_s`
--

CREATE TABLE `t_t1_s` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `c1` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `c2` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `c3` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `c4` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `t_t1_s`
--

INSERT INTO `t_t1_s` (`id`, `c1`, `c2`, `c3`, `c4`, `created_at`, `updated_at`) VALUES
(1, 't1', 't1', 't1', 't1', '2023-12-06 10:41:41', '2023-12-06 10:41:41'),
(2, 't2', 't2', 't2', 't2', '2023-12-06 10:41:41', '2023-12-06 10:41:41');

-- --------------------------------------------------------

--
-- Table structure for table `t_t2_s`
--

CREATE TABLE `t_t2_s` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `c1` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `d1` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `d2` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `d3` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `t_t2_s`
--

INSERT INTO `t_t2_s` (`id`, `c1`, `d1`, `d2`, `d3`, `created_at`, `updated_at`) VALUES
(1, 't1', 'd1', 'd1', 'd1', '2023-12-06 10:42:10', '2023-12-06 10:42:10'),
(2, 't1', 'd2', 'd2', 'd2', '2023-12-06 10:42:10', '2023-12-06 10:42:10');

-- --------------------------------------------------------

--
-- Table structure for table `user_accounts`
--

CREATE TABLE `user_accounts` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `id_number` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `full_name` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `username` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `password` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `section` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `role` varchar(20) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `user_accounts`
--

INSERT INTO `user_accounts` (`id`, `id_number`, `full_name`, `username`, `password`, `section`, `role`, `created_at`, `updated_at`) VALUES
(2, 'TEST-2', 'TEST-2', 'TEST-2', 'TEST-2', 'IT', 'admin', '2023-12-05 21:40:22', '2023-12-06 08:31:22'),
(3, 'TEST-3', 'TEST-3', 'TEST-3', 'TEST-3', 'IT', 'user', '2023-12-05 21:43:01', '2023-12-05 21:43:01'),
(4, '23-12345', 'TESTA101', 'TESTA', 'TESTA', 'IT', 'user', '2023-12-06 08:33:14', '2023-12-06 10:03:54'),
(5, '22-09876', 'TESTE101', 'TESTE', 'TESTE', 'IT', 'user', '2023-12-06 08:33:14', '2023-12-06 10:03:54');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `migrations`
--
ALTER TABLE `migrations`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `t_exercises`
--
ALTER TABLE `t_exercises`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `t_exercises_name_unique` (`name`);

--
-- Indexes for table `t_t1_s`
--
ALTER TABLE `t_t1_s`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `t_t1_s_c1_unique` (`c1`);

--
-- Indexes for table `t_t2_s`
--
ALTER TABLE `t_t2_s`
  ADD PRIMARY KEY (`id`),
  ADD KEY `t_t2_s_c1_index` (`c1`);

--
-- Indexes for table `user_accounts`
--
ALTER TABLE `user_accounts`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `migrations`
--
ALTER TABLE `migrations`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `t_exercises`
--
ALTER TABLE `t_exercises`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `t_t1_s`
--
ALTER TABLE `t_t1_s`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `t_t2_s`
--
ALTER TABLE `t_t2_s`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `user_accounts`
--
ALTER TABLE `user_accounts`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
