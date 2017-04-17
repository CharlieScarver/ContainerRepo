<?php

	// 1h

	$hayStacks[] = $_GET['arrows'];
	$hayStacks[] = $_GET['arrows1'];
	$hayStacks[] = $_GET['arrows2'];
	$hayStacks[] = $_GET['arrows3'];

	//var_dump($hayStacks);

	$arrows['large'] = 0;
	$arrows['medium'] = 0;
	$arrows['small'] = 0;
	$count = 0;
	
	foreach ($hayStacks as $stack) {
		$matchesArr = array();
/*		$matches['large'] = 0;
		$matches['medium'] = 0;
		$matches['small'] = 0;

		preg_match_all("/(>{3}-{5}>{2})/", $stack, $matchesArr);
		$matches['large'] += count($matchesArr[0]);
		$arrows['large'] += $matches['large'];

		preg_match_all("/(>{2}-{5}>{1})/", $stack, $matchesArr);
		$matches['medium'] += count($matchesArr[0]) - $matches['large'];
		$arrows['medium'] += $matches['medium'];

		preg_match_all("/(>{1}-{5}>{1})/", $stack, $matchesArr);
		$matches['small'] += count($matchesArr[0]) - $matches['medium'] - $matches['large'];
		$arrows['small'] += $matches['small'];
*/
		preg_match_all("/(>{3}-{5}>{2})|(>{2}-{5}>{1})|(>{1}-{5}>{1})/", $stack, $matchesArr);
		$count += count($matchesArr[0]);
		
		foreach ($matchesArr[0] as $key => $value) {		
			if ($value === '>>>----->>') {
				$arrows['large']++;
			} else if ($value === '>>----->') {
				$arrows['medium']++;
			} else if ($value === '>----->') {
				$arrows['small']++;
			}
		}

		//print_r($matches);
		//echo "<br/>";
	}

	//echo "<br/>";
	//print_r($arrows);

	$counted = $arrows['small'] + $arrows['medium'] + $arrows['large'];

	$concat = $arrows['small'] . $arrows['medium'] . $arrows['large'];
	$encrypted = decbin($concat) . strrev(decbin($concat));
	$encrypted = bindec($encrypted);

	if ($count == $counted) {
		echo "$encrypted";
	}
