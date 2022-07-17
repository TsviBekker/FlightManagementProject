export const formatDateTime = (date) => {
  const m = new Date(date);
  const dateString =
    m.getUTCHours() +
    ":" +
    m.getUTCMinutes() +
    ":" +
    m.getUTCSeconds() +
    " " +
    m.getUTCDate() +
    "/" +
    m.getUTCMonth() +
    "/" +
    m.getUTCFullYear();
  return dateString;
};
