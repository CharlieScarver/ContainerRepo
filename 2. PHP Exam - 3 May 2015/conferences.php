<?php

	date_default_timezone_set('utc');
	$input = $_GET['conferences'];
	$page = intval($_GET['page']);
	$pageSize = intval($_GET['pageSize']);
//	var_dump($input);

	// Take the ones matching the template
	$matchesArr = [];
	preg_match_all('/(\d{4}(-|\/)\d{2}\2\d{2}), (#(?:[A-Za-z]|-|\.)+), (\'.+\'), ((?:[A-Za-z]|-|,)+), \d+, \d+/', $input, $matchesArr);

	// Save the values
	foreach ($matchesArr[0] as $key => $value) {
		$splitter = split(', ', $value);
		$confs[$key]['date'] = str_replace("/", "-", $splitter[0]);
		$confs[$key]['hashtag'] = $splitter[1];
		$confs[$key]['name'] = str_replace("'", "", $splitter[2]);
		$confs[$key]['location'] = $splitter[3];
		$confs[$key]['tickets'] = intval($splitter[4]) - intval($splitter[5]);
	}

	// Sort the array
	foreach ($confs as $key => $value) {
		$date[$key] = $value['date'];
		$location[$key] = $value['location'];
		$tickets[$key] = $value['tickets'];
		$name[$key] = $value['name'];
	}

	array_multisort($date, SORT_DESC, $location, SORT_STRING, $tickets, SORT_DESC, $name, SORT_STRING, $confs);
//	var_dump($confs);

	// Check the date occurrences
/*	$dateOccurrences = [];
	$passed = [];
	foreach ($confs as $key => $val) {
		for ($i = $key+1; $i < count($confs); $i++) {
		 	if ($val['date'] === $confs[$i]['date'] && !in_array($val['date'], $passed)) {
		 		if (!isset($dateOccurrences[$val['date']])) {
		 			$dateOccurrences[$val['date']] = 1;
		 		}
		 		$dateOccurrences[$val['date']]++;
		 	}
		 }
		 $passed[] = $val['date']; 
	}
*/
//	var_dump($dateOccurrences);

//	echo ($page-1)*$pageSize . "<br/>" . ($page*$pageSize);

	//Print the table
	$latestDate = '';
	echo "<table border=\"1\" cellpadding=\"5\"><tr><th>Date</th><th>Event name</th><th>Event hash</th><th>Days left</th><th>Seats left</th></tr>";
	for ($i=($page-1)*$pageSize; ($i < $page*$pageSize) && (isset($confs[$i])); $i++) { 
		$inside = true;
		echo "<tr>";

		$dateOccurrences = [];
		for ($j=$i; ($j < $page*$pageSize) && (isset($confs[$j])); $j++) { 
			if ($confs[$i]['date'] === $confs[$j]['date']) {
		 		if (!isset($dateOccurrences[$confs[$i]['date']])) {
		 			$dateOccurrences[$confs[$i]['date']] = 0;
		 		}
		 		$dateOccurrences[$confs[$i]['date']]++;
		 	}
		}

		$days = (strtotime($confs[$i]['date']) - strtotime(date('Y-m-d'))) / (60*60*24);
		$days = round($days, 0);
		if ($days >= 0) {
			$days = "+".$days;
		}
		if (isset($dateOccurrences[$confs[$i]['date']]) 
			&& $confs[$i]['date'] !== $latestDate 
			&& $pageSize > 1 
			&& $i < ($page*$pageSize)-1
			&& isset($confs[$i+1])
			&& $confs[$i]['date'] === $confs[$i+1]['date']) 
		{
			echo "<td rowspan=\"".$dateOccurrences[$confs[$i]['date']]."\">".$confs[$i]['date']."</td>";
			$latestDate = $confs[$i]['date'];
		} else if ($confs[$i]['date'] !== $latestDate) {
			echo "<td>".$confs[$i]['date']."</td>";
		}
		echo "<td>".$confs[$i]['name']."</td>";
		echo "<td>".$confs[$i]['hashtag']."</td>";
		echo "<td>".$days." days</td>";
		echo "<td>".$confs[$i]['tickets']." seats left</td>";

		echo "</tr>";
	}

	if (!isset($inside)) {
		echo "<tr>";
		echo "<td>-</td>";
		echo "<td>-</td>";
		echo "<td>-</td>";
		echo "<td>-</td>";
		echo "<td>-</td>";
		echo "</tr>";
	}

	echo "</table>";

	