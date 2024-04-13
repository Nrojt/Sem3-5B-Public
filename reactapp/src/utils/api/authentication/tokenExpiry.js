export default function tokenExpiryInMinutes(tokenExpiryDateTime) {
  // getting the current unix timestamp
  const currentUnixTimestamp = Date.now() / 1000;

  // calculating the expiry time of the token
  const tokenExpiryTimeStamp = new Date(tokenExpiryDateTime).getTime() / 1000;

  // calculating the token expiry time in minutes
  const tokenExpiryTimeInMinutes = Math.floor(
    (tokenExpiryTimeStamp - currentUnixTimestamp) / 60,
  ); // convert from seconds to minutes

  return tokenExpiryTimeInMinutes;
}
